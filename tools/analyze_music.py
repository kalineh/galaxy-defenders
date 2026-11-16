"""Reproducible stage/music timing evidence, using Python's standard library only.

Exact engine events and inferred audio landmarks are deliberately separate.
The HTML report is a local listening worksheet; it never marks cues approved.
"""
import csv
import hashlib
import html
import io
import json
import math
from pathlib import Path
import statistics
import subprocess
import wave

ROOT = Path(__file__).resolve().parents[1]
OUT = ROOT / 'music' / 'timing'
SCRATCH = ROOT / 'artifacts' / 'native-port'
EXE = ROOT / 'build' / 'headless' / 'music-trace.exe'


def run(*args):
    return subprocess.check_output([str(EXE), *map(str, args)], cwd=ROOT).decode()


def onset_signal(features):
    previous = [0.0] * 3
    result = []
    for row in features:
        result.append(sum(max(0, x - p) for x, p in zip(row, previous)))
        previous = row
    mean = statistics.mean(result)
    sd = statistics.pstdev(result) or 1
    return [(x - mean) / sd for x in result]


def align(binary, onset):
    # Strongest separated note-on frames; this is a timing hypothesis, not MIDI recovery.
    strengths = [(sum(binary[i * 8:i * 8 + 8]), i) for i in range(len(binary) // 8)]
    selected = []
    used = set()
    for weight, frame in sorted(strengths, reverse=True):
        if not weight or frame in used:
            continue
        selected.append(frame)
        used.update(range(frame - 3, frame + 4))
        if len(selected) == 500:
            break
    if len(selected) < 30:
        return None

    def score(scale, shift):
        total = count = 0
        for frame in selected:
            j = round(frame * scale + shift)
            if 1 <= j < len(onset) - 1:
                total += max(onset[j - 1:j + 2])
                count += 1
        return total / count if count else -100

    hypotheses = []
    for step in range(-15, 16):
        scale = 1 + step / 1000
        for shift in range(-120, 121, 3):
            hypotheses.append((score(scale, shift), scale, shift))
    best = max(hypotheses)
    fine = [(score(best[1] + d / 10000, best[2] + t), best[1] + d / 10000, best[2] + t)
            for d in range(-5, 6) for t in range(-3, 4)]
    value, scale, shift = max(fine)
    alternative = max(h[0] for h in hypotheses if abs(h[2] - shift) >= 15)
    return {'scale': round(scale, 5), 'offset_seconds': round(shift / 60, 4),
            'onset_score': round(value, 3), 'alternate_offset_score': round(alternative, 3),
            'status': 'candidate; listen before using MIDI-derived times', 'sampled_note_frames': len(selected)}


def pulse_candidates(onset):
    # Autocorrelation cannot resolve beat vs subdivision; retain alternatives.
    peaks = []
    for lag in range(20, 61):
        score = sum(onset[i] * onset[i - lag] for i in range(lag, len(onset))) / len(onset)
        peaks.append((score, lag))
    chosen = []
    for score, lag in sorted(peaks, reverse=True):
        if all(abs(lag - p['lag_frames']) >= 3 for p in chosen):
            chosen.append({'bpm': round(3600 / lag, 2), 'lag_frames': lag, 'correlation': round(score, 3)})
        if len(chosen) == 3:
            break
    return chosen


def landmarks(features, binary, alignment):
    seconds = []
    for start in range(0, len(features), 60):
        block = features[start:start + 60]
        seconds.append([statistics.mean(row[c] for row in block) for c in range(3)])
    changes = []
    for t in range(4, len(seconds) - 4):
        before = [statistics.mean(r[c] for r in seconds[t - 4:t]) for c in range(3)]
        after = [statistics.mean(r[c] for r in seconds[t:t + 4]) for c in range(3)]
        score = sum(abs(math.log((b + .002) / (a + .002))) for a, b in zip(before, after))
        changes.append((score, t, before, after))
    chosen = []
    for score, t, before, after in sorted(changes, reverse=True):
        if score < .45 or any(abs(t - c['seconds']) < 7 for c in chosen):
            continue
        chosen.append({'seconds': t, 'normalized': round(t / (len(features) / 60), 6),
                       'change_score': round(score, 3), 'energy_direction': 'increase' if sum(after) > sum(before) else 'decrease',
                       'status': 'audio-derived candidate; not an approved musical section boundary'})
        if len(chosen) >= 12:
            break
    # Channel entries/exits provide separate evidence without asserting instrument identity.
    channel_changes = []
    if alignment:
        counts = [[sum(binary[i * 8 + c] > 0 for i in range(t * 60, min((t + 1) * 60, len(binary) // 8)))
                   for c in range(8)] for t in range(math.ceil(len(binary) / 480))]
        for t in range(4, len(counts) - 4):
            before = [sum(r[c] for r in counts[t - 4:t]) for c in range(8)]
            after = [sum(r[c] for r in counts[t:t + 4]) for c in range(8)]
            entering = [c for c in range(8) if before[c] == 0 and after[c] >= 6]
            leaving = [c for c in range(8) if after[c] == 0 and before[c] >= 6]
            if entering or leaving:
                time = round(t * alignment['scale'] + alignment['offset_seconds'], 3)
                if not channel_changes or time - channel_changes[-1]['seconds'] >= 4:
                    channel_changes.append({'seconds': time, 'entering_channels': entering, 'leaving_channels': leaving})
    return sorted(chosen, key=lambda x: x['seconds']), channel_changes, seconds


def main():
    OUT.mkdir(parents=True, exist_ok=True)
    SCRATCH.mkdir(parents=True, exist_ok=True)
    trace = run('--stages')
    (OUT / 'stage_events.csv').write_text(trace, encoding='utf-8', newline='\n')
    rows = list(csv.DictReader(io.StringIO(trace)))
    tracks = []
    bins = {p.name.removesuffix('.mid.bin').casefold(): p for p in (ROOT / 'legacy/Content/MusicBin').glob('*.bin')}
    for wav in sorted((ROOT / 'legacy/Content/Music').glob('*.wav')):
        print('Analyzing', wav.stem, flush=True)
        raw = run('--wav', wav)
        (SCRATCH / (wav.stem + '-features.csv')).write_text(raw, encoding='utf-8', newline='\n')
        features = [[float(r[c]) for c in ('low', 'mid', 'high')] for r in csv.DictReader(io.StringIO(raw))]
        with wave.open(str(wav)) as audio:
            duration = audio.getnframes() / audio.getframerate()
            rate, frames = audio.getframerate(), audio.getnframes()
        source = bins.get(wav.stem.casefold())
        binary = source.read_bytes() if source else b''
        assert len(binary) % 8 == 0
        onset = onset_signal(features)
        alignment = align(binary, onset) if binary else None
        cues, channel_changes, energy = landmarks(features, binary, alignment)
        tracks.append({'name': wav.stem, 'wav_sha256': hashlib.sha256(wav.read_bytes()).hexdigest(),
                       'sample_rate': rate, 'sample_frames': frames, 'duration_seconds': round(duration, 6),
                       'binary_file': source.name if source else None, 'binary_seconds': len(binary) / 480 if binary else None,
                       'alignment': alignment, 'pulse_candidates_not_confirmed_bpm': pulse_candidates(onset),
                       'section_candidates': cues, 'channel_change_candidates': channel_changes, 'energy_per_second': energy})
    lookup = {t['name']: t for t in tracks}
    stages = []
    for name in dict.fromkeys(r['stage'] for r in rows):
        events = [r for r in rows if r['stage'] == name and r['kind'] != 'snapshot' and not r['kind'].startswith('unreached_')]
        unreached = [r for r in rows if r['stage'] == name and r['kind'].startswith('unreached_')]
        snapshots = [r for r in rows if r['stage'] == name and r['kind'] == 'snapshot']
        initial = events[0]['music']
        anchors = [{'tick': 0, 'seconds': 0, 'kind': 'track_start', 'name': initial}]
        for r in events:
            if r['kind'] in ('music', 'camera_stop', 'finish', 'secret', 'health_bar') or (r['kind'] == 'enemy' and r['name'].startswith('Boss')):
                anchors.append({'tick': int(r['tick']), 'seconds': round(int(r['tick']) / 60, 6),
                                'kind': r['kind'], 'name': r['name'], 'event': int(r['event']),
                                'music': r['music'], 'music_seconds': round(int(r['music_tick']) / 60, 6)})
        windows = []
        last = max(int(r['tick']) for r in events + snapshots)
        for start in range(0, last, 240):
            spawns = [r for r in events if r['kind'] == 'enemy' and start <= int(r['tick']) < start + 240]
            visible = [int(r['visible_enemies']) for r in snapshots if start <= int(r['tick']) < start + 240]
            windows.append({'seconds': start / 60, 'enemy_spawns': len(spawns), 'types': sorted(set(r['name'] for r in spawns)),
                            'visible_peak_no_fire_run': max(visible, default=0)})
        segments = []
        switches = [(0, initial)] + [(a['tick'], a['name']) for a in anchors if a['kind'] == 'music']
        for i, (tick, track) in enumerate(switches):
            end = switches[i + 1][0] / 60 if i + 1 < len(switches) else last / 60
            duration = lookup[track]['duration_seconds']
            loops = []
            time = tick / 60 + duration
            while time < end:
                loops.append(round(time, 6))
                time += duration
            segments.append({'start_tick': tick, 'start_seconds': tick / 60, 'track': track,
                             'observed_end_seconds': end, 'loop_stage_seconds': loops})
        stages.append({'name': name, 'initial_music': initial, 'activated_events': len(events), 'anchors': anchors,
                       'source_sha256': hashlib.sha256((ROOT / 'legacy/StageDefinitions' / (name + '.cs')).read_bytes()).hexdigest(),
                       'unreached_events': unreached, 'music_segments': segments, 'four_second_windows': windows})
    report = {'schema': 1, 'game_hz': 60, 'seed': 1,
              'method': 'Actual native simulation: stationary invincible solo player, no shooting, secrets disabled. Stop 12 seconds after camera stop or stage completion. Stage time excludes pauses. No audio device.',
              'timing_policy': 'Engine anchors are exact for this run; waveform/NSF-derived MIDI landmarks and BPM are unapproved estimates. Preserve WAV durations and stage event times. Never infer downtime from spawn count alone.',
              'tracks': tracks, 'stages': stages}
    (OUT / 'timing.json').write_text(json.dumps(report, indent=2) + '\n', encoding='utf-8', newline='\n')
    write_report(report, lookup)
    assert len(stages) == 19 and sum(s['activated_events'] + len(s['unreached_events']) for s in stages) == 3448
    print('Wrote music/timing/{timing.json,stage_events.csv,review.html} and MUSIC_TIMING.md')


def write_report(report, lookup):
    lines = ['# Music timing contract', '',
             'Generated by `scripts/analyze_music.sh`. Exact engine anchors are recorded; musical section',
             'boundaries remain review candidates. This is not a completed listening approval.', '',
             'The original tooling mentions MIDI converted from NSF (`nsf2midi`); it does not establish',
             'that these WAVs were rendered from those MIDI files. No original MIDI/NSF source was found',
             'in the checked-in music directories. Eight-byte binary frames retain weighted note-on',
             'activity, not notes, durations, instrument patches or a recoverable score. The converter',
             'also uses a simplistic tempo calculation; do not assume binary timing is authoritative.', '',
             'Run the generator from Git Bash; it uses pinned Zig and Python standard library only.',
             'Open [the listening worksheet](music/timing/review.html) locally for original audio,',
             'clickable candidate timestamps and simultaneous spawn/visibility summaries.',
             '[Machine-readable timing](music/timing/timing.json) contains every candidate and source hash;',
             '[event CSV](music/timing/stage_events.csv) contains actual activation positions/ticks and snapshots.', '',
             '## Rules for replacement scores', '',
             '- Preserve track-start phase, exact transition anchors, loop length and end padding unless a reviewed score explicitly handles them.',
             '- Do not change stage layouts to accommodate a song. Match new musical phrases to the existing timeline.',
             '- Treat boss sections as variable-length loops: kills and the camera-stop wait depend on play.',
             '- Pause/resume, secret detours and stage seeks must restore the appropriate track position.',
             '- An empty spawn window is not necessarily downtime: enemies and bullets may remain onscreen.',
             '- BPM candidates have beat/subdivision ambiguity and are not a locked tempo map.', '',
             '## Simulation and confidence', '', report['method'], '',
             'Event positions are recorded after that simulation tick; activation is not necessarily first',
             'onscreen appearance. Visibility counts include enemy parts and hostile entities, and depend',
             'on this no-fire run. Secret stages are separately timed from entry; normal stages skip detours.',
             'The 60 Hz tick boundary provides 16.7 ms resolution, not a measured audio-output latency.', '',
             'All 3,448 authored events are accounted for: 3,423 activate in this trace; 25 remain',
             'beyond its reachable camera range/end. Unreached events have tick -1 in the CSV and',
             'are explicitly listed per stage in JSON, never assigned an invented playback time.', '',
             '## Stage anchors', '', '| Stage | Initial track | Boss / music switch / camera stop (seconds) |', '| --- | --- | --- |']
    for s in report['stages']:
        values = [f"{a['seconds']:.3f}: {a['kind']} {a['name']}" for a in s['anchors'] if a['kind'] in ('music', 'camera_stop') or a['name'].startswith('Boss')]
        lines.append(f"| {s['name']} | {s['initial_music']} | {'; '.join(values)} |")
    lines += ['', '### Loop boundaries before track switches', '']
    for s in report['stages']:
        for seg in s['music_segments']:
            for t in seg['loop_stage_seconds']:
                lines.append(f"- {s['name']}: `{seg['track']}` wraps to its start at stage {t:.6f}s.")
    lines += ['', '## Original recordings and candidate landmarks', '',
              'Candidate times below come from changes in four-second low/mid/high energy windows.',
              'They are useful listening locations, not automatically identified verses/choruses.', '',
              '| Track | Exact WAV duration (s) | Audio change candidates (s) |', '| --- | ---: | --- |']
    for t in report['tracks']:
        lines.append(f"| {t['name']} | {t['duration_seconds']:.6f} | " + ', '.join(str(c['seconds']) for c in t['section_candidates']) + ' |')
    lines += ['', 'The original music remains credited to RushJet1. Analysis assets are references, not new compositions.', '']
    (ROOT / 'MUSIC_TIMING.md').write_text('\n'.join(lines), encoding='utf-8', newline='\n')
    page = ['<!doctype html><meta charset="utf-8"><title>Galaxy Defenders music timing review</title>',
            '<style>body{font:16px system-ui;background:#111826;color:#dfeafa;max-width:1200px;margin:40px auto;padding:0 24px}table{border-collapse:collapse;width:100%;margin:20px 0}td,th{padding:8px;text-align:left;border-bottom:1px solid #344056}button,a{color:#a9ddff}button{background:#233650;border:1px solid #58728b;padding:7px;cursor:pointer;margin:3px}section{margin:60px 0}small{color:#a9b6c9}.bar{display:inline-block;background:#67b6ce;height:12px}audio{width:100%}</style>',
            '<h1>Original music / stage timing</h1><p>Exact engine anchors; candidate musical boundaries. Click a cue to hear the original around it. No candidate is listening-approved. Spawn lulls can still contain enemies.</p>']
    for index, t in enumerate(report['tracks']):
        name = html.escape(t['name'])
        page += [f'<section><h2>{name}</h2><audio id="a{index}" controls preload="none" src="../../legacy/Content/Music/{html.escape(t["name"], quote=True)}.wav"></audio>',
                 f'<p>{t["duration_seconds"]:.3f}s • pulse candidates (not verified BPM): ' + ', '.join(str(p['bpm']) for p in t['pulse_candidates_not_confirmed_bpm']) + '</p>']
        if t['alignment']:
            a = t['alignment']
            page.append(f'<small>Estimated binary → WAV: time × {a["scale"]} + {a["offset_seconds"]}s. Score {a["onset_score"]}; competing offset {a["alternate_offset_score"]}. Periodic music can give ambiguous alignment.</small>')
        for cue in t['section_candidates']:
            page.append(f'<button onclick="play({index},{cue["seconds"]})">{cue["seconds"]}s: energy {cue["energy_direction"]}</button>')
        for s in report['stages']:
            if s['initial_music'] != t['name']:
                continue
            page.append(f'<h3>{s["name"]} — actual stage run</h3><p>')
            for a in s['anchors']:
                page.append(f'{a["seconds"]:.3f}s {html.escape(a["kind"])} {html.escape(a["name"])}; ')
            page.append('</p><table><tr><th>Stage time</th><th>New enemies / 4s</th><th>Visible peak</th><th>Authored types</th></tr>')
            for w in s['four_second_windows']:
                seg = next(seg for seg in reversed(s['music_segments']) if seg['start_seconds'] <= w['seconds'])
                target = next(i for i, tr in enumerate(report['tracks']) if tr['name'] == seg['track'])
                offset = (w['seconds'] - seg['start_seconds']) % lookup[seg['track']]['duration_seconds']
                page.append(f'<tr><td><button onclick="play({target},{offset})">{w["seconds"]:.0f}s</button><small>{html.escape(seg["track"])} @{offset:.2f}s</small></td><td>{w["enemy_spawns"]} <span class="bar" style="width:{min(w["enemy_spawns"] * 3, 180)}px"></span></td><td>{w["visible_peak_no_fire_run"]}</td><td>{html.escape(", ".join(w["types"]))}</td></tr>')
            page.append('</table>')
        page.append('</section>')
    page.append('<script>function play(i,t){document.querySelectorAll("audio").forEach(a=>a.pause());const a=document.getElementById("a"+i);a.currentTime=Math.max(0,Math.min(t-2,a.duration||t));a.play();}</script>')
    (OUT / 'review.html').write_text('\n'.join(page), encoding='utf-8', newline='\n')


if __name__ == '__main__':
    main()
