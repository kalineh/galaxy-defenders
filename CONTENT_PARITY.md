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
| Standalone content | Asteroid/BigAsteroid console spawns |
| UI/debug | Original labels/art, bundled licensed Noto fonts, console commands, state fades and F3 timing graph |

`./test.sh` passes the campaign, equipment/shop/save,
collision, boss, effects, deterministic RNG and console checks. Campaign tests
exercise authored encounters before scripted kills; they do not prove every fight
is beatable with every loadout. The native build and windowless software renderer
pass, including inspected clear/shop captures. No desktop automation or
interactive playtesting was performed.

Obsolete OldStage definitions, their native support, the empty SpacePlatform
placeholder and unused shader experiments were removed at the user's request.
The campaign contains twelve main and seven bonus stages. Pilot abilities remain
dormant as in the original gameplay; no native editor exists.

Windows is verified. Web/phone packaging, physical controller/disconnect playtests,
and exhaustive frame-by-frame comparison with the legacy runtime remain unverified.
