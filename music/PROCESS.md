# Soundtrack composition process

Track 9 is the listening-approved working reference for coherence and instrument
body, not a motif or instrument bank to stamp onto every song. The other tracks
remain open to composition and timbre changes. Do not copy original melodies.

## Repeat for each track

1. Inspect the original reference. Run `python tools/analyze_music_midi.py` to
   regenerate `../legacy/music_reference_analysis.json`. Compare channel registers, sustained
   versus short notes, recurring attack gaps, delayed entries and density changes.
   Infer an arranging direction; do not mistake converter tempo for musical BPM,
   tiny effect notes for a composed melody, or pitch totals for a proven key.
2. Write a short identity: harmonic center/tension, lead contour, rhythmic gesture,
   accompaniment role and instrument contrast. Give each song its own motif.
3. Compose an eight-bar statement and a complementary eight-bar answer/development.
   Put harmony, bass, melody and drums on one continuous sixteenth grid. Strong
   melody attacks and accompaniment support the current chord; passing notes must
   be intentional. Use rests, held notes and phrase endings, not perpetual scales.
4. Voice the accompaniment beneath/above the lead. Keep one main foreground part;
   when the lead leaves, promote the counter part. Match bass and kick accents,
   shorten fast-note envelopes, and reserve fills for phrase endings. Richness
   comes from complementary spectra, articulation and restrained depth effects.
5. Preserve every protected section time, BPM, later layer switch, source mapping,
   original WAV length and stage hash. A section cut does not restart the beat or
   harmony. Resolve the final phrase without adding time. The original waveform
   landmarks remain candidates; this pass does not claim MIDI/WAV synchronization.
6. Run score harmony/grid/phrase checks, duplicate-hook review, full synth tests,
   and silent offline renders (finite samples, headroom, no stolen voices). Build
   the native game and produce listening previews spanning more than the opening.
   Tests establish mechanical correctness; the human decides whether it is music
   worth keeping. Use `debug.music` and direct normalized seeks for listening.
7. Record the actual changes and validation, commit and push the verified piece.
   Listen for melody first, then bass/chord support, groove, balance and fatigue.
   Fix the relevant layer; do not randomize notes to manufacture uniqueness.

## Reference evidence and composition direction

MIDI seconds below are raw converter times, not synchronized stage cues. These are
arranging inferences from measured events, not claims of having auditioned the
originals. Full counts, openings and source hashes are in ../legacy/music_reference_analysis.json.

| Track | Original evidence | New composition direction |
| --- | --- | --- |
| 01 Departure | Voyage high line has repeated ~133 ms attacks; low fifth/octave bass enters around 12.05 s | E-minor launch, rolling high answer and broad syncopated brass melody |
| 02 Signal Grid | 1-X-0 starts with percussion; bass at ~26.87 s and rapid paired upper voices at ~39.67 s | C-sharp-minor machine groove, clipped FM calls, octave bass and bII tension |
| 03 Skyward Assault | Aerial Assault opens with short low repeated notes; upper channels enter around 28.5 s with close rhythmic offsets | F-minor low brass attack, solid pedal rhythm, open upper replies |
| 04 Night Watch | Afraid of Darkness alternates ~100/67 ms upper gates over ~333 ms bass; later held notes contrast with rapid effects | D-minor nocturne, dotted short/long figures, warm reed and shadowed plucks |
| 05 Alkaline Skies | Alkali Earth repeats the opening line ~300 ms later on another channel and later adds many layers | G-minor interlocking mallet melody, delayed answering gestures, active octave bass |
| 06 Dreadnought | No MIDI; original duration and measured audio section candidates available | B-centered boss riff, semitone pressure, power chords and restrained metal accents |
| 07 Predator Orbit | Predator upper motif has a ~150 ms delayed partner; initial bass alternates long C/G holds before short pulses | F-sharp-minor stalking cell, space between calls, hollow FM lead and pulse replies |
| 08 Homeward | Insurrection contrasts a long high opening note with a lower held-note melody; bass/drums enter near 30.08 s | A-minor defiant theme at half-time, broad pulse lead and low restrained accompaniment |
| 09 Control Circuit | Fast upper motion, lower held melody, descending bass, later clipped riff | Keep the accepted title composition and layered instrument pass |
| 10 Uprising | Luminosity opens with a ~12 s low note beneath rapid upper figures and a later broader melody | E-flat-minor pedal atmosphere, glass counter figure and sustained lead arc |
| 11 Moon Base | Math Party repeats a low motif in multiple registers, commonly ~133 ms spacing | A-flat-minor clipped unison character, 3+3+2 accents within a stable bar, FM clav |
| 12 Luminous Wake | Sirius alternates octave-register gestures and very fast upper effects above long bass holds | B-minor spacious melody, octave replies, sustained bass body and narrow bright pulse |
| 13 Clockwork Orbit | Still Afraid has a one-note delayed partner and early C/B bass alternation | B-flat-minor pursuit, leading-tone diminished tension, dry plucks and quick answers |
| 14 Sirius Drive | Hidden Answer uses a recurring high fifth between moving lower tones; bass adds short octave motions | C-minor discovery, bright mallet foreground and a quiet upper pedal-like reply |
| 15 Afterimage | Troubled Dreams mixes ~117/133 ms attacks, repeated upper notes and an octave bass figure | E-minor unease, held reed melody against restless soft pulses and displaced bass |
| 16 Hidden Passage | Try Harder begins with ~1.6 s harmonized notes and descending upper movement; bass comes later | D-minor resolve, broad descending brass line, clipped responses and firm cadence |
| 17 Dream Fracture | Turbo begins with repeated low ~100 ms attacks and a fifth-related partner | C-sharp-minor motor riff, occasional Dorian brightness, saw/pulse bite and short bass |
| 18 Another Flight | No MIDI; original duration and measured audio section candidates available | G-centered reflective closing theme, seventh chords, minor-plagal color and soft FM keys |
| 19 Overdrive | Fighting for Control pairs repeated ~333/500 ms upper attacks over ~167 ms octave bass | C-minor sustained rhythmic stabs over an urgent bass engine, contrasting second phrase |

The missing MIDI references do not block new composition. Their mood choices are
explicit creative decisions, not recovered facts about the original notes.

## Album pass implementation

All eighteen non-title scores now contain sixteen explicitly authored melody bars
and chord-matched counter/bass patterns. The development changes register, contour,
note spacing or held-versus-clipped articulation; it is not a random permutation.
Bar selection keeps the phrase and harmony together. Closing bars use a tonic /
dominant / tonic cadence, except the reflective seventh-chord track 18, which
closes through C major / C minor / G. No original note sequences are imported.

Each lead has a complementary counter timbre. Counter velocity drops while the
lead plays and rises at the existing lead-out cuts. Pads stay quiet; short attacks
use short releases; bass/kick anchors support the same beat. Layered pulses are
used where body helps, while FM brass, reeds, keys and mallets retain distinct
roles. Noninteger FM ratios that produced a metallic clash have been avoided in the
new banks so accidental dissonance is not built into every note.

The checks now examine eight-note melodic runs across bar boundaries rather than
forbidding common four-note vocabulary. Every pitched accompaniment attack and
strong-beat melody attack must belong to the current chord; melody/bass/counter
patterns cannot overlap their own next attack. These are guardrails, not a
substitute for listening or a ban on intentional future suspensions/syncopation.

## Verification of the previous pass

The native Windows build and full test suite pass. All nineteen rendered lengths
match the original recordings at 48 kHz; all BPM values, section cuts, later layer
switches and original stage/timing hashes are retained. The accepted title render
is byte-identical. Album render peaks range from 0.3214 to 0.3781, RMS from 0.0625
to 0.0953, with finite samples and no voice stealing. Seek determinism is checked
after the main arrangement enters for every track. Other platforms and live-device
playback were not exercised. These are engineering checks, not listening approval.

Run `python tools/music_listening_report.py` after a native/render build for the
local worksheet with full new tracks, protected-cut jumps and original reference
players. Nothing autoplays. All source links and seek bounds are CLI-validated;
humans own interactive playback and the musical review.

## Current album revision

The eighteen non-title tracks have a new four-bar lead statement. In longer
tracks the earlier statement returns on alternate 16-bar cycles, so the first
and second laps answer differently over the same chords. Where a counter part
carries a section on its own, it gets a separate line. Predator Orbit leaves a
long space after the first high call. Skyward Assault punches twice in the low
register before the octave jump. The title keeps its high motor and lower riff.

Each kit has an individually tuned snare, hat and answering-bar accents. The
twelve campaign tracks have short written drum pickups at the documented boss
handoffs. Afterimage's handoff falls about 1.25 seconds into its second lap,
so its opening uses a sparse kick pickup. Another Flight traces E to E-flat over C major and C minor, then lands on D
over G in the short final bar.
