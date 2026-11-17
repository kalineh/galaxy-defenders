"""Read a standard MIDI note reference without audio devices or extra packages."""
import collections
import json
from pathlib import Path
import struct
import hashlib
import statistics

def read_midi(path):
    data=Path(path).read_bytes()
    assert data[:4]==b'MThd'
    header=struct.unpack('>I',data[4:8])[0]
    fmt,tracks,ppq=struct.unpack('>HHH',data[8:14])
    assert fmt in (0,1) and not ppq&0x8000
    pos=8+header; events=[]
    for track in range(tracks):
        assert data[pos:pos+4]==b'MTrk'
        length=struct.unpack('>I',data[pos+4:pos+8])[0]
        buf=data[pos+8:pos+8+length];pos+=8+length
        i=tick=running=0
        def vlq():
            nonlocal i
            value=0
            while True:
                byte=buf[i];i+=1;value=(value<<7)|(byte&127)
                if byte<128:return value
        while i<len(buf):
            tick+=vlq(); status=buf[i]
            if status>=128:i+=1
            else:status=running
            assert status
            if status==255:
                kind=buf[i];i+=1;n=vlq();body=buf[i:i+n];i+=n
                events.append((tick,track,'meta',kind,body));running=0
            elif status in (240,247):
                n=vlq();i+=n;running=0
            else:
                running=status;kind=status>>4;ch=status&15;n=1 if kind in (12,13) else 2
                body=buf[i:i+n];i+=n;events.append((tick,track,kind,ch,body))
    events.sort(key=lambda e:e[:2])
    seconds=0;tick=0;tempo=500000;active=collections.defaultdict(collections.deque)
    notes=[];meta=[];counts=collections.Counter();programs=[]
    for at,track,kind,ch,body in events:
        seconds+=(at-tick)*tempo/ppq/1000000;tick=at
        if kind=='meta':
            if ch==81:tempo=int.from_bytes(body,'big');meta.append({'seconds':seconds,'tempo_us':tempo})
            elif ch in (1,2,3,4,6):meta.append({'seconds':seconds,'type':ch,'text':body.decode('latin1')})
            elif ch in (88,89):meta.append({'seconds':seconds,'type':ch,'bytes':list(body)})
            continue
        counts[f'{kind:X}']+=1
        if kind==12:programs.append((round(seconds,4),ch,body[0]))
        if kind==9 and body[1]:active[ch,body[0]].append((seconds,body[1]))
        elif kind==8 or (kind==9 and not body[1]):
            if active[ch,body[0]]:
                start,vel=active[ch,body[0]].popleft()
                notes.append({'start':round(start,6),'end':round(seconds,6),'channel':ch,'note':body[0],'velocity':vel})
    unclosed = sum(len(v) for v in active.values())
    return {'unclosed_note_ons':unclosed, 'format':fmt,'ppq':ppq,'end_seconds':seconds,'meta':meta,'event_counts':dict(counts),'programs':programs,'notes':sorted(notes,key=lambda x:(x['start'],x['channel']))}

ROOT = Path(__file__).resolve().parents[1]
NAMES = ['C', 'C#', 'D', 'Eb', 'E', 'F', 'F#', 'G', 'Ab', 'A', 'Bb', 'B']


def note_name(pitch):
    return NAMES[pitch % 12] + str(pitch // 12 - 1)


def summarize(path):
    midi = read_midi(path)
    channels = []
    for channel in sorted({n['channel'] for n in midi['notes']}):
        notes = [n for n in midi['notes'] if n['channel'] == channel]
        durations = [n['end'] - n['start'] for n in notes]
        gaps = [round(b['start'] - a['start'], 3) for a, b in zip(notes, notes[1:])
                if b['start'] > a['start']]
        pitch_seconds = collections.Counter()
        for n in notes:
            pitch_seconds[NAMES[n['note'] % 12]] += n['end'] - n['start']
        channels.append({
            'channel': channel, 'notes': len(notes),
            'first_seconds': notes[0]['start'],
            'range': [note_name(min(n['note'] for n in notes)), note_name(max(n['note'] for n in notes))],
            'median_gate_seconds': round(statistics.median(durations), 4),
            'common_onset_gaps_seconds': collections.Counter(gaps).most_common(4),
            'pitch_class_seconds': {k: round(v, 2) for k, v in pitch_seconds.most_common()},
            'opening': [[n['start'], note_name(n['note']), round(n['end']-n['start'], 4)] for n in notes[:16]],
            'notes_per_eight_seconds': [sum(start <= n['start'] < start+8 for n in notes)
                                       for start in range(0, int(midi['end_seconds'])+1, 8)]})
    return {'path': path.relative_to(ROOT).as_posix(),
            'sha256': hashlib.sha256(path.read_bytes()).hexdigest(),
            'duration_seconds': round(midi['end_seconds'], 6),
            'unclosed_note_ons': midi['unclosed_note_ons'],
            'meta': midi['meta'], 'event_counts': midi['event_counts'], 'channels': channels}


def main():
    scores = json.loads((ROOT / 'music/album.json').read_text())
    timing = json.loads((ROOT / 'music/timing/timing.json').read_text())
    normalize = lambda name: name.lower().replace(' ', '_')
    files = {normalize(p.stem): p for p in (ROOT / 'legacy/PreprocessFFT/PreprocessFFT/Content').glob('*.mid')}
    tracks = []
    for number, score in enumerate(scores, 1):
        source = score['reference_duration']
        row = {'track': number, 'name': score['name'], 'source': source}
        path = files.get(normalize(source))
        row['midi'] = summarize(path) if path else None
        audio = next(t for t in timing['tracks'] if t['name'] == source)
        row['audio_duration_seconds'] = audio['duration_seconds']
        row['audio_section_candidates'] = audio['section_candidates']
        tracks.append(row)
    output = {'method': 'Direct NSF-converted MIDI inspection; note gates exclude unclosed note-ons. '
              'Pitch bends/controllers are counted but not applied to pitches. MIDI times are not WAV-synchronized. '
              'Converter tempo is not musical BPM; pitch totals do not establish a key. '
              'Missing MIDI uses existing audio timing evidence, not invented note analysis.', 'tracks': tracks}
    path = ROOT / 'legacy/music_reference_analysis.json'
    path.write_bytes((json.dumps(output, indent=2) + '\n').encode())
    print(f'Analyzed {sum(t["midi"] is not None for t in tracks)}/19 MIDI references: {path}')


if __name__ == '__main__':
    main()
