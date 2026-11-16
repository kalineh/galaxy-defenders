# Music analysis and synth audition

Normal gameplay still uses the original RushJet1 recordings. The new score is an
original instrument/arrangement study, not a transcription or approved replacement.
No stage layout or campaign music assignment has changed.

## Audition

Build with `./build.sh native`, launch with `./play.sh`, open the backtick console:

```text
debug.music             # list tracks and override status
debug.music 1           # start Departure study
debug.music 1 0.5       # start halfway through its 120.498646-second loop
debug.music 1 0.94      # hear the approach section
debug.music off         # restore normal music
```

The override survives stage changes and game resets, plays in menus and while
paused, and respects music/master volume. Gameplay, saves and SFX are unaffected.
Normal music continues silently under the override so it can resume when disabled.
Headless/muted runs accept the command but never create an audio device.
Progress must be finite and between 0 and 1; 1 means the final sample, then loop.
Seeking prerolls the score to restore its exact phase/envelope/delay state; a late
seek may briefly stall the debug UI (up to a few seconds on the development PC).

`./scripts/render_music.sh` writes a standalone 48 kHz stereo WAV to
`artifacts/native-port/departure-study.wav`, without a window/audio device.
Pass a different output path as the first argument. The renderer uses the same
synth as the live audition. This temporary WAV is regenerated, not committed.

## Sound and score

`src/synth.c` has 24 fixed voices and nine patches: four-operator FM bass/lead,
band-limited pulse accents, FM bells, detuned pad voices, pitch-swept kick/tom,
noise/tone snare and metallic/noise hats. Envelopes, delayed vibrato, tone filtering,
two synthesis substeps per output sample, equal-power panning and stereo feedback
delay provide the palette. It is a hybrid 16-bit-inspired synth, not console-chip
emulation. The mix has soft saturation/headroom and short loop-edge fades.

The callback advances an integer sample clock and allocates nothing. Stop/seek
uses raylib's audio synchronization before changing synthesis state. Rendering is
independent of game FPS. Original audio files/attribution remain intact.

Edit `prototype.json`, then run `python tools/compile_music.py` to regenerate
`src/synth_score.inc`. The score contains explicit motifs, chords, section times,
arrangement and note articulation; there is no runtime random composition.
Its 150 BPM is a new composition choice, **not a verified original tempo**.
Section starts at 4/11/19/35/113 seconds borrow analysis candidates for audition;
they are not yet approved as the original song's structural boundaries. The
119.383333-second handoff is an exact Stage1 engine anchor. Duration matches the
original recording to the nearest 48 kHz sample.

## Timing evidence and remaining review

See [MUSIC_TIMING.md](../MUSIC_TIMING.md) and open `timing/review.html` locally.
The worksheet provides original audio, candidate cue buttons and actual stage
spawn/visibility summaries. Run `./scripts/analyze_music.sh` to reproduce it.
The analysis uses pinned Zig and Python's standard library, with no extra packages.

Engine anchors/loop lengths are measured. Musical phrase boundaries, exact tempo
maps, instrument quality and playback latency still need listening review. MIDI
activity alignment is explicitly tentative, including competing periodic offsets.
Do not promote the study to campaign music or change stages based on those estimates.

`./test.sh` includes whole-score finiteness/headroom/DC/stereo/voice-capacity checks,
sample-identical seeks across different rendering block sizes, loop endpoints,
score regeneration and console parsing/state isolation. Native and memory-render
builds are checked separately. Automated checks do not claim live-device audition.
