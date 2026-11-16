# Completed music rewrite

All 19 scores now have authored lead/answer phrases, bass/counter parts, chord
cycles, grooves, opening orchestration and per-song instrument banks/mix.
Removed shared compiler riffs, stock backbeat/bell/fill and fixed delay defaults.

Timing protected by music/timing/score_constraints.json: existing BPM, source
mapping, every section cut and later-section layer changes are unchanged. Source
recordings/stage definitions and the switch/loop evidence are hash-checked.

Verified: full ./test.sh; added all-track patch-bank seek check; native build;
windowless software-render check; all 19 full renders finite with headroom and
zero voice stealing, durations exactly match reference resampling to 48 kHz.
No copied/transposed patterns, shared four-note lead/counter hooks, duplicate
rotated chord cycles, drum grooves or complete instrument banks in score checks.
Human listening remains necessary for musical quality and subjective distinction.
Use debug.music to audition the rebuilt album. Legacy music is never played.
