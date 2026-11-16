# Native content parity

The 2026-11-15 source audit's identified active-campaign defects are repaired.
See CONTENT_AUDIT.md for the per-content findings, corrections and evidence.
This is not a claim of exhaustive frame-identical gameplay across all inputs.

| Area | Implemented and verified |
| --- | --- |
| Weapons | All 24 variants/level tables, original emitter offsets, motion, targeting, collision dispatch, particles, sprites and sound mappings |
| Players | Original one-death survival, contact/reciprocal damage, impulse, shields, powerups, sidekicks and pilot loadouts |
| Enemies/bosses | All regular enemies and Boss1-12/parts; audited attack waits/aim, phase thresholds, collision geometry, special interactions and death effects |
| Shop | Release seeded stock, price ordering, purchases/upgrades, firing previews, co-op departure and cancellation |
| Campaign | Twelve main and seven bonus stages, 3,448 authored events, original assets/music, secret entry/return and ending |
| Progression | Timed clear flyout/fades, percentages, awards, medals, versioned atomic saves, Continue and replay selection |
| Scenery | Original layers/colors, animated gradients, depth ordering, incremental particles and shared .NET-compatible seeded random stream |
| Archived content | Asteroid/BigAsteroid console spawns; twelve OldStage definitions (1,835 events), eleven playable |
| UI/debug | Original labels/art, bundled licensed Noto fonts, console commands, state fades and F3 timing graph |

`./test.sh` passes the campaign, archived-stage playback, equipment/shop/save,
collision, boss, effects, deterministic RNG and console checks. Campaign tests
exercise authored encounters before scripted kills; they do not prove every fight
is beatable with every loadout. The native build and windowless software renderer
pass, including inspected clear/shop/archive captures. No desktop automation or
interactive playtesting was performed.

`game.stage OldStage1` through `OldStage12` (also IDs 20..31) access archives.
OldStage4 preserves an original unnamed building at (325.51, -16555.24), and the
loader reports that source defect. Its intended texture is awaiting user input.
SpacePlatform.cs is an empty namespace, not missing gameplay. Unused editor shader
experiments and pilot abilities remain archived/dormant; no native editor exists.

Windows is verified. Web/phone packaging, physical controller/disconnect playtests,
and exhaustive frame-by-frame comparison with the legacy runtime remain unverified.
