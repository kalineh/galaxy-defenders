"""Compile the explicit audition arrangement into sample-addressed C events."""
import argparse
import json
import math
from pathlib import Path
import wave

ROOT = Path(__file__).resolve().parents[1]
RATE = 48000


ROLES = ['bass', 'lead', 'arp', 'bells', 'pad', 'kick', 'snare', 'hat', 'tom']


def validate_timing(scores):
    import hashlib
    constraints = json.loads((ROOT / 'music/timing/score_constraints.json').read_text())
    timing_path = ROOT / 'music/timing/timing.json'
    if hashlib.sha256(timing_path.read_bytes()).hexdigest() != constraints['timing_sha256']:
        raise ValueError('Timing evidence changed; explicit timing review required')
    timing = json.loads(timing_path.read_text())
    for stage in timing['stages']:
        path = ROOT / 'legacy/StageDefinitions' / (stage['name'] + '.cs')
        if hashlib.sha256(path.read_bytes()).hexdigest() != stage['source_sha256']:
            raise ValueError('Stage layout changed: ' + stage['name'])
    assert len(scores) == len(constraints['tracks']) == 19
    references = {t['name']: t for t in timing['tracks']}
    for score, lock in zip(scores, constraints['tracks']):
        if (score['reference_duration'] != lock['source'] or score['bpm'] != lock['bpm'] or
            [[s['start'], s['end']] for s in score['sections']] != lock['sections']):
            raise ValueError('Protected timing changed: ' + score['name'])
        shifts = [{k:s[k] for k in ('bass','lead','arp','bells','pad','drums')} for s in score['sections'][1:]]
        if shifts != lock['layer_shifts']:
            raise ValueError('Protected arrangement shift changed: ' + score['name'])
        path = ROOT / 'legacy/Content/Music' / (lock['source'] + '.wav')
        if hashlib.sha256(path.read_bytes()).hexdigest() != references[lock['source']]['wav_sha256']:
            raise ValueError('Reference recording changed: ' + lock['source'])


def compile_events(score):
    with wave.open(str(ROOT / 'legacy/Content/Music' / (score['reference_duration'] + '.wav'))) as wav:
        frames = round(wav.getnframes() * RATE / wav.getframerate())
    step = 60 / score['bpm'] / 4
    events = []
    def note(time, duration, role, pitch, velocity, pan=0):
        frame = round(time * RATE)
        if frame >= frames or duration <= 0:
            return
        assert 0 <= role < 9 and 0 < pitch < 128 and 0 < velocity < 128 and -100 <= pan <= 100
        events.append((frame, max(1, round(duration * RATE)), role, pitch, velocity, pan))
    previous_end = 0
    for section_index, section in enumerate(score['sections']):
        start, end = section['start'], section['end']
        assert abs(start - previous_end) < .0001 and end > start
        previous_end = end
        song_clock = score.get('clock') == 'song'
        i = math.ceil(start / step - 1e-9) if song_clock else 0
        while (i * step if song_clock else start + i * step) < end - .000001:
            time = i * step if song_clock else start + i * step
            # Continue harmony across section changes instead of restarting the intro.
            bar = i // 16 if song_clock else int(time / (step * 16))
            phrase_bar = score['bar_order'][bar] if 'bar_order' in score else bar
            chord_index = phrase_bar % len(score['roots'])
            root, chord = score['roots'][chord_index], score['chords'][chord_index]
            for role_index, role in enumerate(ROLES):
                enabled = section.get(role, False)
                if role in ('kick', 'snare', 'hat', 'tom'):
                    enabled = section['drums'] >= {'kick':1,'snare':2,'hat':2,'tom':3}[role]
                if not enabled:
                    continue
                part = score['parts'][role]
                slot = i % part['steps']
                # Keep sustained notes across orchestration cuts if the part stays on.
                limit = end
                if song_clock and section_index + 1 < len(score['sections']):
                    following = score['sections'][section_index + 1]
                    if following.get(role, False):
                        limit = min(frames / RATE, (bar + 1) * 16 * step)
                if role == 'pad':
                    if slot in part['onsets']:
                        pitches = score.get('voicings', [])[chord_index] if 'voicings' in score else [root+24+x for x in chord]
                        for j, pitch in enumerate(pitches):
                            note(time, min(step * part['gate'], limit-time), role_index,
                                 pitch, part['velocity'], (j-1)*40)
                    continue
                patterns = part.get('variants', {}).get(section.get('part_set'), part['patterns'])
                pattern_number = phrase_bar if song_clock and part['steps'] == 16 else i // part['steps']
                pattern = patterns[(pattern_number + (0 if song_clock else section_index)) % len(patterns)]
                for onset, pitch, length, velocity in pattern:
                    if slot != onset:
                        continue
                    if role in ('lead','bells') or part.get('pitch_base') == 'tonic':
                        pitch += score['tonic'] + part['register']
                    elif role in ('bass','arp'):
                        pitch += root + part['register']
                    velocity = max(1, round(velocity * section.get('velocity_scale', {}).get(role, 1)))
                    note(time, min(step*length, limit-time), role_index, pitch, velocity, part.get('pan',0))
            i += 1
    assert abs(previous_end * RATE - frames) <= 1
    events.sort()
    return events, frames


def generate_track(score, index):
    events, frames = compile_events(score)
    lines = ['/* Generated by tools/compile_music.py from music/album.json. */',
             f'static const SynthEvent score_{index}[] = {{']
    lines += ['    {' + ','.join(map(str, e)) + '},' for e in events]
    lines += ['};', '']
    return '\n'.join(lines), len(events), frames


def generate():
    scores = json.loads((ROOT / 'music/album.json').read_text())
    validate_timing(scores)
    lines, entries, total = [], [], 0
    for i, score in enumerate(scores):
        text, count, frames = generate_track(score, i)
        lines.append(text)
        entries.append('    {' + json.dumps(score['name']) + ',' + json.dumps(score['reference_duration']) + f', {frames}u, {count}u, score_{i}, {round(RATE * 60 / score["bpm"] * score["mix"]["delay_beats"])}u, {score["mix"]["wet"]}f, {score["mix"]["feedback"]}f' + '},')
        total += count
    lines += ['static const Patch patches[SYNTH_TRACK_COUNT][9] = {']
    for score in scores:
        delay = round(RATE * 60 / score['bpm'] * score['mix']['delay_beats'])
        assert 0 < delay < 32000
        lines.append('    {')
        for role in ROLES:
            values = score['instruments'][role]['parameters']
            assert len(values) == 10 and 0 <= values[0] <= 10
            lines.append('        {' + str(values[0]) + ',' + ','.join(f'{float(x)}f' for x in values[1:]) + '},')
        lines.append('    },')
    lines += ['};', 'static const SynthTrack synth_tracks[] = {', *entries, '};', '']
    return '\n'.join(lines), total


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--check', action='store_true')
    args = parser.parse_args()
    content, count = generate()
    path = ROOT / 'src/synth_score.inc'
    if args.check:
        if path.read_text() != content:
            raise SystemExit('Synth score stale; run python tools/compile_music.py')
    else:
        path.write_text(content, encoding='utf-8', newline='\n')
    print(f'PASS: {count} sample-addressed notes; all original recording durations retained')
