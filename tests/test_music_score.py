"""Guard timing and catch a return to transposed copies of one album template."""
import copy
import importlib.util
import itertools
import json
from pathlib import Path
import unittest

ROOT = Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location('compile_music', ROOT / 'tools/compile_music.py')
compiler = importlib.util.module_from_spec(spec)
spec.loader.exec_module(compiler)


class MusicScoreTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.scores = json.loads((ROOT / 'music/album.json').read_text())

    def test_protected_timing(self):
        compiler.validate_timing(self.scores)
        for edit in ('bpm', 'boundary', 'source', 'layers'):
            changed = copy.deepcopy(self.scores)
            if edit == 'bpm':
                changed[0]['bpm'] += 1
            elif edit == 'boundary':
                changed[0]['sections'][0]['end'] += .1
            elif edit == 'layers':
                changed[0]['sections'][1]['bass'] = not changed[0]['sections'][1]['bass']
            else:
                changed[0]['reference_duration'] = 'Sirius'
            with self.assertRaises(ValueError):
                compiler.validate_timing(changed)

    def test_no_transposed_pattern_copies(self):
        for role in ('lead', 'bass', 'arp'):
            signatures = []
            for score in self.scores:
                part = score['parts'][role]
                notes = part['patterns'][0]
                origin = notes[0][1]
                signatures.append((part['steps'], tuple((t, p-origin, gate) for t, p, gate, _ in notes)))
            self.assertEqual(len(set(signatures)), 19, role)

    def test_no_shared_eight_note_melody(self):
        # Common short figures are vocabulary, not evidence of a recycled song.
        # Compare substantial melodic runs, including across bar boundaries.
        hooks = []
        for score in self.scores:
            part = score['parts']['lead']
            groups = [part['patterns'], *part.get('variants', {}).values()]
            phrases = set()
            for patterns in groups:
                notes = [(bar * part['steps'] + t, p) for bar, pattern in enumerate(patterns)
                         for t, p, _, _ in pattern]
                intervals = [(b[0]-a[0], b[1]-a[1]) for a, b in zip(notes, notes[1:])]
                phrases.update(tuple(intervals[i:i+7]) for i in range(len(intervals)-6))
            hooks.append(phrases)
        for i, j in itertools.combinations(range(19), 2):
            self.assertFalse(hooks[i] & hooks[j], f'lead: tracks {i+1}/{j+1}')

    def test_album_grid_and_harmony(self):
        for score in self.scores:
            self.assertEqual(score['clock'], 'song', score['name'])
            events, frames = compiler.compile_events(score)
            step_frames = compiler.RATE * 60 / score['bpm'] / 4
            for frame, gate, role, pitch, _, _ in events:
                step = round(frame / step_frames)
                self.assertLessEqual(abs(frame - step * step_frames), .501, score['name'])
                self.assertLessEqual(frame + gate, frames + 1)
                if role >= 5:
                    continue
                phrase = score['bar_order'][step // 16]
                chord = {(score['roots'][phrase] + x) % 12 for x in score['chords'][phrase]}
                if role != 1 or step % 4 == 0:
                    self.assertIn(pitch % 12, chord, (score['name'], frame, role, pitch))
            self.assertEqual(score['roots'][score['bar_order'][-1]] % 12, score['tonic'] % 12)
            lead = score['parts']['lead']
            self.assertGreaterEqual(len(lead['patterns']), 8)
            for role in ('bass', 'lead', 'arp'):
                part = score['parts'][role]
                for pattern in part['patterns']:
                    ordered = sorted(pattern)
                    for a, b in zip(ordered, ordered[1:]):
                        self.assertLessEqual(a[0] + a[2], b[0], (score['name'], role, a, b))

    def test_distinct_grooves_and_palettes(self):
        grooves, palettes, harmonies = set(), set(), set()
        for score in self.scores:
            grooves.add(json.dumps([score['parts'][r] for r in ('kick', 'snare', 'hat')], sort_keys=True))
            palettes.add(json.dumps(score['instruments'], sort_keys=True))
            roots, chords = score['roots'], score['chords']
            rotations = [tuple(((roots[(j+k) % len(roots)] - roots[j]) % 12,
                                tuple(chords[(j+k) % len(roots)]))
                               for k in range(len(roots))) for j in range(len(roots))]
            harmonies.add(min(rotations))
            for part in score['parts'].values():
                patterns = part.get('patterns', []) + [p for group in part.get('variants', {}).values() for p in group]
                for pattern in patterns:
                    for onset, _, gate, velocity in pattern:
                        self.assertTrue(0 <= onset < part['steps'] and gate > 0 and 0 < velocity < 128)
        self.assertEqual(len(grooves), 19)
        self.assertEqual(len(palettes), 19)
        self.assertEqual(len(harmonies), 19)

    def test_title_grid_and_harmony(self):
        title = self.scores[8]
        self.assertEqual(title['reference_duration'], "Konami's_Moon_Base")
        events, frames = compiler.compile_events(title)
        step_frames = compiler.RATE * 60 / title['bpm'] / 4
        for frame, gate, role, pitch, _, _ in events:
            step = round(frame / step_frames)
            self.assertLessEqual(abs(frame - step * step_frames), .501)
            self.assertLessEqual(frame + gate, frames + 1)
            if role >= 5:
                continue
            # D minor, with C-sharp in the A-major dominant.
            self.assertIn(pitch % 12, (0, 1, 2, 4, 5, 7, 9, 10))
            phrase = title['bar_order'][step // 16]
            chord = {(title['roots'][phrase] + x) % 12 for x in title['chords'][phrase]}
            # The lead may use passing notes on weak eighths; accompaniment and
            # quarter-beat melody attacks must support the sounding chord.
            if role != 1 or step % 4 == 0:
                self.assertIn(pitch % 12, chord, (frame, role, pitch))
        self.assertEqual(title['roots'][title['bar_order'][-1]] % 12, title['tonic'] % 12)
        # A complete bar after the 18-second cut must use the clipped riff.
        bar = 13
        phrase = title['bar_order'][bar]
        actual = [round(frame / step_frames) % 16 for frame, _, role, _, _, _ in events
                  if role == 1 and round(frame / step_frames) // 16 == bar]
        expected = [n[0] for n in title['parts']['lead']['variants']['drive'][phrase]]
        self.assertEqual(actual, expected)


if __name__ == '__main__':
    unittest.main()
