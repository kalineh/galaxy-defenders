# Content parity

The legacy C# source remains the specification. All 19 authored stage streams and
all enemy/weapon types now run in the native build. This is not yet a claim of
frame-identical gameplay or complete presentation parity.

| Area | Native state | Remaining verification / work |
| --- | --- | --- |
| Weapons | Original level tables; all primary, support and sidekick variants | Audit special collision/effect/audio behavior |
| Equipment | All generators/shields and nine chassis; pilot starting equipment | Chassis/pilot special abilities and full visual HUD details |
| Shop | Six categories, purchases, trade-ins, levels, energy/shield effects, co-op ready | Stage-specific stock, sample ships, exact layout/transitions |
| Enemies | All regular types, hostile projectile kinds, Boss1-Boss12 and parts | Detailed attack timing/movement traces, death art and sound |
| Stages | All 12 main and seven bonus stages; authored 3,448 events; original assets | Playtest balance, secret entrance reachability and fade/return timing |
| Progression | Clear -> shop -> next main stage; secret return restores encounter; original final ending | Saves/Continue, replay selection, stage-clear flyout and explosions |
| Scenery | Original textures, gradients, foreground layers, deterministic cloud motion | Animated blending gradients and exact legacy RNG sequencing |
| Audio | Original title/combat/pause/ending tracks and music-event seeking | Remaining weapon/enemy/pickup sound mapping |
| Economy | Working purchases and score transfer | Original coin pickups, salvage and per-enemy rewards |
| UI/debug | Original menu labels/art, licensed Noto fonts, scriptable console, F3 timing graph | Remaining menu transitions and HUD state details |
| Platforms | Windows native/headless/software renderer verified | Web/phone packaging and physical controller tests |

Circle collisions retain the original `r1*r1 + r2*r2` threshold. EnemyShot uses
its original 40-pixel visual and radius 2; unshielded player radius is 16. Laser
collision geometry uses its original small box with swept detection between ticks.
Boss component collision offsets apply to both contact and projectile tests.

Automated campaign checks run all 19 stages with no dropped spawns and exercise
all boss phases; they do not establish that every fight has been beaten with each
pilot/loadout. Humans own interactive playtesting. No generated substitute waves
or fallback enemy types are used.
