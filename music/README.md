# Synth soundtrack

All campaign, boss, bonus, title/shop, pause, game-over and ending music now uses
the C synth album. Legacy recordings remain analysis references and are never
loaded for playback. SFX remain the original assets.

## Developer album

Open the console and enter `debug.music`. Up/Down selects and immediately plays
one of 19 tracks. Left/Right seeks by 5% of its duration, Enter restarts, Escape
closes the album and restores the current game's synth music. The album freezes
gameplay and its background music while open and uses the bundled console font. It is not a player menu.

`debug.music 1..19 [0..1]` also auditions directly; `debug.music off` restores the
game soundtrack. Volume follows music/master settings. Headless/muted runs do
not initialize audio. Seeking is immediate through pre-rendered PCM streams.

## Scores and build

Edit `album.json`, then run `python tools/compile_music.py`. It contains each
track's title, source mapping, tempo, motifs, chord cycle and section arrangement.
All 19 arrangements have individually authored melodic sentences, bass/counter
patterns, chord cycles, drum grooves, openings and instrument banks. They remain
drafts for listening review, not transcriptions or approved final compositions.
Candidate section cuts come from the analysis; original recording lengths are
preserved to the nearest 48 kHz sample. Stage data and music-switch events are
unchanged. See ../MUSIC_TIMING.md for exact anchors versus tentative musical cues.

Native/render builds compile the C renderer and cache WAVs in build/music, then
copy them beside the executable under assets/synth. No generated WAV is committed.
Initial builds take longer to render the album; subsequent builds reuse it.
The 24-voice C synth has nine part slots per song, each with its own patch.
FM, pulse, saw, triangle, bell/pad voices and synthesized drums are available.
Instrument kinds/envelopes/gains/filtering/vibrato and tempo-linked delay mix
are authored in album.json; oscillator implementation remains in src/synth.c.

`./scripts/render_music.sh` renders track 1 standalone. The built renderer also
accepts `OUTPUT.wav TRACK_ID`. Audio renders are checked for finite samples,
headroom and voice stealing. ./test.sh checks sample-identical seek/chunk output,
loop endpoints, console parsing and game-state isolation. Musical quality and
live device playback still require human listening; automated checks are silent.

## Album-wide identity rewrite

Removed the shared root/third/fifth pulse, fixed bass ostinato, stock lead pair,
repeated bell accents, universal backbeat/fill and fixed 150-BPM delay. The compiler
now schedules explicit parts; it has no fallback that silently inserts a riff.
Harmony continues across section cuts instead of restarting every intro.
Shared synthesis primitives/drum sounds are intentional; complete instrument banks,
grooves and phrases differ. Automated comparison detects transposed copies and
shared four-note pitch/rhythm hooks; it does not prove subjective distinctiveness.

### Protected timing

music/timing/score_constraints.json freezes the pre-rewrite BPM choices, track
mappings, every section boundary and later-section layer shifts. Openings have
new orchestration. The compiler rejects drift in these values, original recording
hashes, stage source hashes or the timing evidence containing switch/loop anchors.
WAV lengths retain the nearest 48 kHz sample. Original candidate musical boundaries
remain candidates; this rewrite does not claim to have confirmed them by listening.

### Listening guide

| Track | Identity | Lead / counter sound |
| --- | --- | --- |
| 01 Departure | Open fifths, clipped brass calls and offbeat engine bass | brass / square |
| 02 Signal Grid | Dry circuit bleeps, chromatic cell and broken electro beat | clav / organ |
| 03 Skyward Assault | Skyward brass fanfare and syncopated octave launches | brass / brass |
| 04 Night Watch | Breathy reed nocturne, long rests and half-time sub bass | reed / hollow |
| 05 Alkaline Skies | Bright mallet jazz extensions over a loping bass line | hollow / clav |
| 06 Dreadnought | Distorted saw siege riff, tritone stabs and double-kick pressure | saw / clav |
| 07 Predator Orbit | Low inharmonic bell warning, semitone tension and empty space | reed / glass |
| 08 Homeward | Round organ hook with delayed answer and rolling hand-drum pulse | organ / reed |
| 09 Control Circuit | Minor-key launch, fast upper pulse, descending bass and clipped later riff | pulse / pulse |
| 10 Uprising | Glass cascade against a displaced snare and wide stereo strings | glass / hollow |
| 11 Moon Base | Clocked pluck in a 3+3+4+3+3 accent cycle | clav / square |
| 12 Luminous Wake | Airy string melody, suspended voicings and shimmering high replies | strings / glass |
| 13 Clockwork Orbit | Staccato metal plucks, pickup notes and restless sixteenth hats | clav / hollow |
| 14 Sirius Drive | Hollow marimba discovery motif over soft triangle bass | hollow / piano |
| 15 Afterimage | Slow detuned string descent and distant bell echoes | strings / reed |
| 16 Hidden Passage | Resilient reed call with a marching offbeat response | reed / organ |
| 17 Dream Fracture | Fast pulse chase, ascending fifths and clipped clavinet response | square / clav |
| 18 Another Flight | Quiet electric-piano farewell, long release and no driving ostinato | piano / hollow |
| 19 Overdrive | Saw-and-square motor riff, broken kick grid and chromatic turnaround | saw / square |

Instrument parameter arrays are: kind, attack seconds, decay seconds, sustain,
release seconds, gain, modulator ratio, FM index, low-pass coefficient, vibrato.
Kinds 0/1 are FM, 2 pulse, 3 bell, 4 detuned pad, 5 kick, 6 snare, 7 hat, 8 tom,
9 band-limited saw, 10 triangle, 11 layered pulse. For layered pulse, ratio sets
the sine-body frequency multiplier. Part events are [sixteenth onset,
pitch/interval, gate in sixteenths, velocity]. Lead/bell intervals use the song tonic; bass/counter
intervals follow the current chord root. Pattern lengths and registers are explicit.

## Title-screen composition (track 9 only)

The previous title counter-line used a fixed major third over minor chords, and
section-local part counters ran against song-global harmony. Track 9 now opts
into `clock: song`: all parts keep the same sixteenth grid across the fixed cuts
at 5, 18, 29 and 112 seconds. The other eighteen scores are unchanged.

The title now uses original Dm-C-Bb-A material: a fast high pulse above a slower,
lower melody, then a clipped lead/bass riff from 18 seconds. The minor center,
descending bass and major dominant replace the previous relaxed C-Am-F-G cadence.
Accompaniment attacks and strong-beat melody notes are chord tones; weak notes
can connect them. The high pulse gets quieter once the main riff takes over.
A Dm-A-Dm coda returns to the broader theme. The title's later layer flags were
explicitly revised for this reorchestration; the boundaries, BPM, original timing
evidence and exact length remain unchanged. Other tracks retain their prior locks.

The title uses layered pulses: a main pulse, a quiet detuned square and a sine
body (an octave below the lead, in unison for the high pattern). Both pulse
oscillators use band-limited edges. A fuller FM bass, slightly stronger quiet pad
and modest stereo delay support them. This changes timbre/mix, not note events;
all layers fit within one synth voice per event. Other tracks may adopt or adapt
this palette after listening; their current sound is not a musical constraint.
Human listening remains the musical-quality check.

`voicings` supplies absolute pad notes; `bar_order` selects the harmonic/melodic
bar together, including the coda. A part's `pitch_base: tonic` permits an explicit
counter melody rather than blindly adding one interval set to every chord root.
`velocity_scale` balances roles per section without moving their entry/exit times.
Optional section `part_set` selects matching part `variants`; otherwise the part's
normal `patterns` apply. Only the title uses this to switch theme/riff vocabulary.
Sustained parts that remain enabled can finish their notes across a section cut;
parts being removed are still gated at that cut. No notes extend past the loop.

## Original title note reference recovered

The actual NSF-converted MIDI is checked in at
`legacy/PreprocessFFT/PreprocessFFT/Content/Konami's_Moon_Base.mid`.
The earlier search missed this directory; the reduced MusicBin files are not
our only reference. The MIDI metadata credits RushJet1, Capcom and nsf2midi.
This is a conversion, not an original authoring project or authoritative timbre map.

Direct event inspection: channel 0 opens with F#5, C#6, F#6, G#6, C#6, F#6,
G#6, C#6, B6, C#6, B6, A#6, C#6, A#6, G#6, F#6, at roughly 167 ms per note.
Channel 1 enters in a lower register with longer melodic notes. Channel 2 starts
with roots F#2, E2, D#2, D2, using fifth/octave gestures. At MIDI time ~18.35s,
channels 1/2 move into short F#/E/A/D-based riffs with ~133 ms note spacing.
Those are MIDI times, not verified WAV sync markers. The file has startup events,
117.25s total duration versus 116.053333s audio, and two final unclosed note-ons.
Its 30-BPM tempo event represents the converter's timing grid, not musical tempo.

This evidence suggests a high running ostinato against a slower melody, descending
chromatic bass tension and a later harder riff. It is a more useful direction
than the earlier replacement's relaxed C-major cadence. The rebuilt title uses
these broad arranging ideas with newly authored motifs, different harmony and a
new instrument balance. It does not reuse the MIDI melody, ostinato or bass line.
The original MIDI-to-WAV alignment remains unverified; the replacement preserves
the existing documented timing anchors rather than treating raw MIDI seconds as
new section boundaries.
