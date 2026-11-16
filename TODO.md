# Completed title-screen music pass

Only track 9 (Control Circuit / Konami's_Moon_Base) rewritten. Fixed major-third
counter notes over minor harmony and section-local parts against global chords.
Title now uses one song-wide grid, bar-matched C-Am-F-G theme/response, smooth
voicings, stable bass/backbeat and C-G-C coda. Accompaniment/delay reduced.

Verified: only track 9 score and generated event stream changed; other 18 identical.
Existing section cuts, layer shifts and exact duration retained. Tests check the
continuous sample grid, diatonic notes, accompaniment/strong-beat chord membership,
loop endpoint and protected timing. Full suite, native and memory-render pass;
all renders finite with headroom/no voice stealing. Human listening still required.

Audition debug.music 9, or artifacts/native-port/title-theme-preview.wav (30 sec).
Other tracks retain their prior scheduling/composition; this is the listening
reference before considering a wider musical pass.

Original title MIDI found in legacy/PreprocessFFT/PreprocessFFT/Content. Initial
claim that only MusicBin activity survived was incorrect. See music/README.md
for measured note/rhythm evidence. No music changed during reference analysis;
next musical pass should restore original tension/drive rather than major-key
background-music energy. Scratch note CSV/JSON: artifacts/native-port/title-midi.*.
