# Content parity

The latest item-by-item source review is in [CONTENT_AUDIT.md](CONTENT_AUDIT.md).
It records concrete remaining gameplay differences, including player respawns,
contact damage, projectile interactions and boss timing. The earlier audits below
approve only their stated sprite/geometry/attachment scope.

The legacy C# source remains the specification. All 19 authored stage streams and
all enemy/weapon types now run in the native build. This is not yet a claim of
frame-identical gameplay or complete presentation parity.

| Area | Native state | Remaining verification / work |
| --- | --- | --- |
| Weapons | Original level tables; all primary, support and sidekick variants | Audit special collision/effect/audio behavior |
| Equipment | All generators/shields and nine chassis; pilot starting equipment | Full visual HUD details; unused legacy abilities remain dormant |
| Shop | Six categories, purchases, trade-ins, levels, energy/shield effects, co-op ready | Stage-specific stock, sample ships, exact layout/transitions |
| Enemies | All regular types, hostile projectile kinds, Boss1-Boss12 and parts | Regular and Boss1-Boss12 sprite/geometry/attachment audit complete; detailed timing and effect traces remain |
| Stages | All 12 main and seven bonus stages; authored 3,448 events; original assets; all secret entrances verified | Playtest balance and fade/return timing |
| Progression | Clear -> shop -> next main stage; secrets; ending; solo/co-op saves and Continue; unlocked replay selection | Stage-clear flyout and explosions |
| Scenery | Original textures, gradients, foreground layers, deterministic cloud motion | Animated blending gradients and exact legacy RNG sequencing |
| Audio | Original title/combat/pause/ending tracks and music-event seeking | Remaining weapon/enemy/pickup sound mapping |
| Economy | Purchases, coin/powerup drops, magnet collection, original health-based rewards | Salvage/player death behavior |
| UI/debug | Original menu labels/art, licensed Noto fonts, scriptable console, F3 timing graph | Remaining menu transitions and HUD state details |
| Platforms | Windows native/headless/software renderer verified | Web/phone packaging and physical controller tests |

Circle collisions retain the original `r1*r1 + r2*r2` threshold. EnemyShot uses
its original 40-pixel visual and radius 2; unshielded player radius is 16. Laser
collision geometry uses its original small box with swept detection between ticks.
Boss component collision offsets apply to both contact and projectile tests.
Ordinary impacts apply chassis resistance and shield overflow, without an invented
post-hit immunity interval. Shield and armor feedback use distinct original sounds
and particles; armor impacts apply the legacy velocity response. Enemy contact
still needs health-based/reciprocal damage and its separate collision impulse.

Automated campaign checks run all 19 stages with no dropped spawns and exercise
all boss phases; they do not establish that every fight has been beaten with each
pilot/loadout. Humans own interactive playtesting. No generated substitute waves
or fallback enemy types are used.

Regular-enemy audit (2026-11-15): checked all regular enemy/part definitions
against their legacy sprite names, animation frames, collision dimensions and
relative layers. Restored Pyramid/Cutter overlays, Spike/FenceBeam animation,
Pyramid fragments, Cutter/Swirl attachment transforms, linked Fence teardown,
stationary turret corpses, and Glob/BlackHole draw order. Tests cover the shape
matrix, spawned part positions/velocities, blocking and fence cleanup. Windowless
galleries cover all regular body sprites; this is not a frame-by-frame AI/effects
comparison. Enemy contact damage and special death/teleport particles still need
the separate gameplay/effects pass noted in TODO.md.

Boss audit (2026-11-15): compared all 12 bosses and their active parts with the
legacy sprite/origin/depth and collision definitions. Rendered each boss at three
health states using the windowless renderer and inspected the captures.

| Bosses | Findings and corrections |
| --- | --- |
| 1-3 | Restored body sublayers below their parts; retained original circle/AABB dimensions. |
| 4-5 | Arms behind bodies; covers in their own upper layer. Boss4 cover still disappears below 60% health. |
| 6 | Body, rotating center and spheres use separate layers; center rotation now accelerates from rest with the original interpolation. |
| 7 | Confirmed cannon pivot origins; corrected half-pixel body collision X offset. |
| 8 | Confirmed single body sprite and unrotated AABB; unused collision-sphere definitions remain inactive, as in the original. |
| 9 | Confirmed bar/ball/body layers; initial balls start at full arm length and replacement timers are independent. |
| 10 | Blocks grow/shrink instead of popping; corrected block collision offsets/corners; balls use the entity layer. Shooting balls no longer incorrectly powers them. |
| 11 | Confirmed arm/hand/body order and collision shapes. |
| 12 | Confirmed six arms behind body and two eyes above it, with the original eye origins and body collision offset. |

Round projectile versus rectangular enemy/boss collisions now use rounded
corners rather than the previous square-expanded approximation. Tests exercise
outside/inside corner shots, boss/part shape and layer tables, Boss6 opening,
Boss9 replacement timing, Boss10 block fades and collision enablement, and Boss12
non-colliding visual parts. All original enemy/boss texture files match the legacy
assets byte-for-byte. Full boss attack/movement timing, special particles and
human difficulty/controller playtests remain outside this visual/geometry audit.
