# Completed title instrument pass

Title track 9 now uses layered pulses (main pulse, quiet detuned square, sine
body), fuller FM bass, slightly stronger pad and modest stereo delay. All notes,
gates, velocities, pans, section times and stage timing remain unchanged.

Verified: native build and full ./test.sh pass, including deterministic title
seek after the lead/bass entry. Full title render has no voice stealing or
clipping: peak 0.3781, RMS 0.0898, DC 0.001888, length 5,570,560 frames at 48 kHz.
Other eighteen renders currently identical. Their sound is not protected: the
user considers the other compositions unfinished and open to broader improvement.
Keep stage timing constraints when revisiting them; use title as a working reference.

Audition debug.music 9 or artifacts/native-port/title-theme-preview.wav (35 s).
Comparison: artifacts/native-port/title-before-texture.wav. Human listening is
the remaining musical-quality review. No interactive playtest performed.
Original MIDI-to-WAV alignment remains unverified; see music/README.md.
