# Active work

Request: map all original music against stage playback; preserve song structure
without changing layouts. Then build a richer small C synth prototype and add
`debug.music 1 0.5` normalized audition override plus `debug.music off`.

## Completed implementation

- Implemented headless stage trace and standard-library audio/binary timing analysis.
- Analysis verified: all 19 recordings and stages; 3,448 authored events accounted
  for (3,423 triggered, 25 unreachable in the bounded run). Stage1 captures at
  19/35/113 seconds inspected. PCM 60 Hz/band probe passes.
- MUSIC_TIMING.md and music/timing contain exact stage anchors, loop times,
  waveform/channel candidates and a local listening worksheet. Musical section
  candidates still require listening review; no invented approvals.
- Timing analysis committed/pushed as aed483d.
- Added 24-voice C synth, nine instrument patches, explicit original study score,
  offline renderer and independent debug.music audition override.
- Full test suite passes; latest DSP adjustment passed core and whole-score synth
  checks. Native/software-render builds pass. Seek/chunk output is sample-identical.
- Audition: debug.music 1 0.5; debug.music off restores normal audio.
  music/README.md documents score editing and waveform rendering.
- Temporary listening file: artifacts/native-port/departure-study.wav.
- Keep original music as default. Candidate section boundaries/BPM must not be
  called listening-approved or exact; binary data is derived from NSF-to-MIDI.

Human musical approval, physical audio latency, web/phone builds and interactive
playtesting remain separate verification steps.
