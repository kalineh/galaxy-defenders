# Game content source audit

Audited 2026-11-15 against native commit `a4d69ab` and the checked-in `legacy/`.
This is the requested code comparison, not a claim based on campaign smoke tests.
No gameplay code was changed by this audit.

**The game is not yet behavior-equivalent.** The previous fixes covered sprite
layers, geometry and attachments; they did not establish full content parity.
Several differences below are gameplay defects, not just missing polish.

`OK` means the explicitly named scope matches the source. `Not OK` means a
specific source difference exists. For a failing item, the listed difference is
sufficient to reject parity; it is not an exhaustive list of every possible
frame-order difference. Shared defects are referenced rather than counted as a
separate implementation bug for every affected item. Dormant legacy code is
identified separately so it does not become an invented feature requirement.

## Shared findings

| ID | Verdict | Source comparison and consequence |
| --- | --- | --- |
| C1 | Not OK | `legacy/Enemy/Enemy.cs::OnCollide(CShip)` sends `min(2.5 + HealthMax, 6)` damage and receives 0.5 damage (10 for an invincible ship). `src/game.c::update_enemies` always starts from 4 damage and never damages the enemy. `Ship.TakeCollideDamage` also applies an outward impulse before ordinary impact handling; the port omits it. |
| C2 | Not OK | Legacy collision collection tests current shapes after entity updates (`World.UpdateEntitiesSingleThreadCollision`, `Collision.cs`). Native `update_bullets` sweeps shots between positions and processes hostile/player shots in pool order. Sweeps can hit objects that the original discrete test misses. This is a behavior change even where dimensions now match. |
| W1 | Not OK | `legacy/Ship/Ship.cs::InitializeEquipment` adds 20 to each primary emitter's local X offset. Native `weapon.c::spawn` uses the table offset directly. Normal primary fire starts 20 pixels farther back. Focus emitters do not receive this adjustment in legacy. |
| W2 | Not OK | Native `update_bullets` emits `EFFECT_LASER_HIT` for every enemy/building hit. Legacy projectile classes have distinct hit effects and trails; MiniShot has no such particle burst. `app.c` also chooses textures by weapon kind instead of damage, omitting BigLaser/BigFlame selection. |
| W3 | Not OK | Native pellet/missile blocking dispatches every player weapon through one generic handler. Legacy has distinct overloads: EnemyMissile has no Blade/Beam/Boomerang/Vulcan/DrunkMissile/Lightning/ChargeShot handlers; EnemyPellet has no ChargeShot handler. Bomblet survives a pellet hit in legacy but is consumed natively. |
| W4 | Not OK | Legacy draws both sidekick ships and charge particles (`Ship.Draw`, `Weapon.Charge`). Native declares a sidekick texture but does not load/draw it, and has no weapon-charge effect. Applies to all six sidekick choices. |
| E1 | Not OK | Native `enemy_die` always emits one standard death effect with fixed velocity. Legacy base deaths use random drift plus conditional anti-camera motion; several enemy subclasses add or replace effects. |
| B1 | Not OK | All bosses lose their multi-explosion death sequence (14 bursts per color for Boss1, for example). Clearing enemies/projectiles by setting `active=false` bypasses their legacy death callbacks. Boss scoring is not a zero-score defect: legacy caches the score when setting HealthMax, before later setting BaseScore to zero. |
| P1 | Not OK | Native `game_join`/`game_damage_player` give three lives and immediate full-health respawns. Legacy `Ship.OnDie` removes the ship; `World.CheckGameOverConditions` starts a 120-frame game-over sequence when no ships remain. This changes solo/co-op survival, difficulty and secret-stage deaths. |
| S1 | Not OK | Native shop availability ignores the current stage and owned inventory. Legacy `ShopMenu` combines owned parts with `Map` node stock and sorts by price. Sample-ship weapon previews and fly-to-stage behavior are absent. |
| S2 | Not OK | Native completion immediately opens a clear menu. Legacy `World.UpdateStageEnd` flies ships out, emits trails, fades, and gates confirmation after counter 380. Bonus return is immediate natively; legacy waits past 30 frames, fades, and adjusts the return camera by 280. |

## Weapons and projectiles

The **definition tables are OK** for every row: `python tools/import_equipment.py
--check` passes for all 24 variants and all emitter levels. That covers prices,
reload values, offsets, damage, energy and custom parameters, not their runtime
interpretation. The verdict below covers the weapon as played and rendered.

| Weapon variant | Verdict | Confirmed blocker |
| --- | --- | --- |
| FrontLaser | Not OK | W1; W2 damage-dependent BigLaser selection absent. |
| FrontLaserFocus | Not OK | W2: always Laser rather than choosing BigLaser at damage >= 0.4. |
| SpreadLaser | Not OK | W1; W2 damage-dependent sprite selection. |
| SpreadLaserFocus | Not OK | W2 damage-dependent sprite selection. |
| Plasma | Not OK | W1; plasma trail/hit/splash particles replaced by W2. |
| PlasmaFocus | Not OK | W2; native splash lifetime is one tick with no `WasHit` state; legacy splash survives until hit or age > 2. Native draws it as another plasma sprite; legacy splash has no Visual. |
| Flame | Not OK | W1/W2. Legacy uses per-emitter previous `FireRotation`, then updates it from owner age; native uses global tick directly. Legacy random sprite spin and age-based growth are absent. |
| FlameFocus | Not OK | Same Flame trajectory/spin/effect differences; BigFlame selection absent. |
| Vulcan | Not OK | Vulcan hit effect replaced by W2; incorrect EnemyMissile interaction (W3). |
| Bomblet | Not OK | Destroyed on pellet contact instead of surviving it (W3). Expiry disables the bullet without the legacy hit effect/sound. |
| Lightning | Not OK | W1. Native `nearest_enemy` excludes buildings; legacy `DoBounce` includes them and moves position along both old and new velocity at a bounce. Native omits those position steps. |
| LightningFocus | Not OK | Same missing building targets and bounce position steps. |
| Beam | Not OK | W1; hit particles appear at the beam's origin rather than the struck target. Legacy looping Cue ownership is replaced by a one-shot fire sound. |
| BeamFocus | Not OK | Legacy sprite rotates by `AliveTime * 0.25`; native rotation is fixed. Legacy hit effect location and looping audio are absent. |
| Missile | Not OK | Legacy starts sprite rotation at firing angle minus pi; native leaves the firing angle. Trail and missile hit effect absent (W2). |
| SeekBomb | Not OK | Legacy prefers an unclaimed seekable target, retains it until invalid/offscreen, and excludes `CanSeekerTarget=false`. Native has no claim tracking and can select Boss10Ball or non-colliding boss visuals. |
| Boomerang | Not OK | Legacy random initial sprite rotation replaced by fire angle; hit and terminal particles missing. W3. |
| DrunkMissile | Not OK | Native computes changing trajectory angle but never writes it to displayed `rotation`; legacy rotates the sprite with its trajectory. Trail/hit effects missing. |
| MiniShot | Not OK | W4; generic laser hit particles added where legacy only plays a sound. |
| ChargeShot | Not OK | W4; charge-specific impact effect replaced by W2; wrong pellet/missile blocking (W3). |
| Blade | Not OK | W4. Legacy adds signed 0.15 rotation each tick and vertically flips the left blade; native omits both. |
| MissileVolley | Not OK | W4 and the DrunkMissile trajectory/display/effect defects. |
| MissileLauncher | Not OK | W4 and the Missile sprite rotation/trail defects. |
| LaserShot | Not OK | W4; damage-based laser sprite selection omitted. |

Sources: `legacy/Weapon/`, `legacy/Projectile/`, `legacy/Ship/Ship.cs` versus
`src/weapon.c`, `src/game.c::update_bullets`, `src/app.c::draw_world` and audio playback.
All player projectiles additionally inherit C2; shop purchases inherit S1.

| Hostile projectile | Verdict | Confirmed blocker |
| --- | --- | --- |
| EnemyShot | Not OK | C2; native consumes shots during player spawn immunity, whereas legacy ignore-bullets handling returns before destruction. |
| EnemyMiniShot | Not OK | Same shared collision/lifetime differences as EnemyShot. |
| EnemyLaser | Not OK | Legacy 1 x 0.5 AABB with top-left at position is treated as a radius-0.5 circle against the player. |
| EnemyCannonShot | Not OK | Legacy 11 x 42 AABB at offset (-6,-21), four hit health, three-frame hit delay and extra outward force. Native uses radius 21 and destroys the shot on the first player contact. |
| EnemyMissile | Not OK | Native selects nearest player every tick; legacy retains its target until death. Trail/explosion effects missing; W3. |
| EnemyPellet | Not OK | Radius/spin were corrected, but ChargeShot and Bomblet interactions still differ (W3). |

## Regular enemies and attached parts

Every row was checked against its named `legacy/Enemy/<name>.cs` plus inherited
`Enemy.cs`; native paths are `enemy.c`, `enemy_extra.c` and `game.c`.
Rows that cite only C1/E1 have a proven shared failure; they are not assertions
that all remaining AI timing is correct. Earlier corrected geometry is retained.

| Enemy / part | Verdict | Confirmed blocker |
| --- | --- | --- |
| Ball | Not OK | C1, E1. |
| Isosceles | Not OK | C1, E1. Its disabled legacy shooting call correctly remains disabled. |
| Turret | Not OK | C1, E1. |
| MiniSquare | Not OK | C1; legacy half-size death effect replaced by standard size. |
| BigBall | Not OK | C1; extra scale-1.5 death bursts omitted. |
| DownTurret | Not OK | C1, E1. |
| ShootBall | Not OK | C1, E1. |
| Airship | Not OK | C1; extra scale-1.5 death bursts omitted; fires the incorrect EnemyLaser collision shape. |
| Beamer | Not OK | C1, E1; EnemyLaser collision mismatch. |
| BlackHole | Not OK | Center, pull and ship-pull particles absent. Native pulls EnemyMissile too, although legacy BlackHole has no EnemyMissile overload. |
| Cannon | Not OK | C1, E1; EnemyCannonShot shape/repeat-hit defects above. |
| Cutter | Not OK | C1; native silently removes balls on death instead of calling their `Die` paths. |
| CutterBall | Not OK | Inherits legacy ship-contact handling but native uses C1's fixed damage. |
| DoubleTurret | Not OK | C1, E1. |
| Fence | Not OK | C1, E1; SeekBomb's collision handler still damages it despite legacy `CanSeekerTarget=false` guard. |
| FenceBeam | Not OK | Legacy explicitly deals 2 collision damage; native sends it through the fixed-4 path. |
| Glob | Not OK | Native reselects nearest player even after attachment; legacy retains `AttachTarget`. Native uses distance < 32 instead of shape contact, never snaps at the legacy 12-pixel grip threshold, and silently disappears at timer end. |
| MissilePod | Not OK | C1, E1 and EnemyMissile defects. |
| MissileStorm | Not OK | C1, E1 and EnemyMissile defects. |
| MissileTriple | Not OK | C1, E1 and EnemyMissile defects. |
| Pyramid | Not OK | C1; additional death bursts absent. Fragment HealthMax reassignment in legacy reapplies difficulty scaling; native multiplies the already-scaled value only by 1.5. |
| RaidTurret | Not OK | C1, E1 and pellet interaction defects. |
| RotateTurret | Not OK | C1, E1; corrected layered turret rendering does not address these inherited failures. |
| Spike | Not OK | Legacy calls base contact, applies an additional 7.5 impact damage, emits explosions and dies. Native only applies generic contact and keeps Spike alive. |
| Splitter | Not OK | C1, E1; previous fragment placement/motion fixes remain in place. |
| SplitterFragment (left/right) | Not OK | C1, E1. Both sprite choices exist. |
| Swirl | Not OK | C1, E1. |
| SwirlBall | Not OK | Contact does not forward reciprocal damage to its owner as legacy inherited contact plus overridden `TakeDamage` would. |
| Teleporter | Not OK | Vanish/appear effects and countdown spin acceleration missing. Legacy exit velocity includes direction toward screen center and friction 0.99; native adds a fixed downward 2 and omits friction. |
| TripleShot | Not OK | C1, E1. |
| BonusShip | Not OK | C1, E1. |

## Bosses and components

Each body fails B1 and C1 independently of its additional findings below.
The visual/geometry corrections in `CONTENT_PARITY.md` are narrower successes,
not full boss approval. Sources are each numbered legacy boss file versus
`src/enemy.c` (1-2), `src/boss.c` (3-12), and shared death/collision code.

| Boss | Verdict | Additional concrete difference |
| --- | --- | --- |
| Boss1 | Not OK | B1: its explicit 14 paired death explosions and child death callbacks are absent. |
| Boss2 | Not OK | B1: explicit multi-explosion death sequence replaced by one normal effect. |
| Boss3 | Not OK | Native changes attack phase immediately from health while legacy changes weapon routines at burst boundaries with startup waits. Chunk spin jumps to -0.01 per tick instead of interpolating angular velocity. |
| Boss4 | Not OK | Legacy starts sinusoidal movement after 120 frames (40 horizontal, 20 vertical); native has no body movement routine. Cover-break and arm-retraction explosions missing. |
| Boss5 | Not OK | One hit crossing both 80% and 50% thresholds only activates arms in legacy's `TakeDamage` if/else-if; native immediately enters phase 2 and opens the cover. Native threshold handling also occurs after co-op damage scaling, whereas legacy tests the incoming damage before base scaling. |
| Boss6 | Not OK | Phase-0 sweep case 0 waits 2 between shots in legacy, native defaults to 3. Sphere attack omits the temporary orbit-speed boost to 0.2 and 60-frame windup. |
| Boss7 | Not OK | Small cannons should wait 30 then aim for 15 frames at a captured target before firing; native continuously tracks and fires first at 30. Big cannon also captures aim per burst in legacy rather than every tick. Body movement omits phase-0 pauses. |
| Boss8 | Not OK | Wind force lacks the two `Boss8Wind` particle streams. Threshold handling is based on post-scaled health natively versus the incoming-damage test before base co-op scaling in legacy. |
| Boss9 | Not OK | B1; launched ball replacement geometry fixes do not restore boss death behavior. |
| Boss10 | Not OK | Ball contact omits the 60-frame repeat-contact guard, post-bounce position step and ship recoil. Ball trail missing. SeekBomb can target balls contrary to legacy. |
| Boss11 | Not OK | Hand spin rest is fixed at 45 frames natively; legacy selects 30/45/60. Hand cannon sequence uses quarter-turn order instead of legacy offsets right, left, down, up. Body omits phase-0 movement pauses. |
| Boss12 | Not OK | Last-phase paired spread uses left/right/left/right offsets natively; legacy uses left/right/right/left for cannon indices 0/1/2/3. |

| Component | Verdict | Blocker / checked scope |
| --- | --- | --- |
| Boss1Chunk | Not OK | C1 and omitted extra death effects. |
| Boss3Chunk | Not OK | Spin interpolation and inherited C1. |
| Boss4Chunk | Not OK | Retraction checks every tick rather than legacy 30-frame polling; retraction effects absent. C1. |
| Boss4Cover | Not OK | Corrected layer; break effects absent. |
| Boss5Chunk | Not OK | C1 and omitted death callbacks when the body dies. |
| Boss5Cover | OK | Scope: separate layer and quarter-turn opening, initial/target angle and 120-step rotation agree with legacy. |
| Boss6Sphere | Not OK | Windup/orbit-speed behavior and destruction effects absent. C1. |
| Boss6Center | OK | Scope: separate layer and interpolated core rotation constants match the legacy core visual. |
| Boss7CannonBig | Not OK | Captured burst aim replaced by continuous tracking. |
| Boss7CannonSmall | Not OK | Initial wait/aim sequence differs. |
| Boss8Collision | OK | Dormant legacy definition; neither authored fight creates these collision spheres. |
| Boss8Pusher | Not OK | Native applies the radial ship force inline but omits both wind particle streams. |
| Boss9Ball | Not OK | C1; body death deactivates attached balls without their death behavior. |
| Boss9Bar | OK | Scope: non-colliding bar follows body position/orbit angle on the corrected lower layer. |
| Boss10Ball | Not OK | Missing contact guard/recoil/trail; wrong seekability. |
| Boss10Block | Not OK | Corrected geometry/fade does not fix interactions with the ball's missing post-bounce position step. |
| Boss11Hand | Not OK | Fixed spin rest and changed firing-origin order. C1. |
| Boss11Arm | Not OK | Shared boss damage on shots exists; contact reciprocal damage still missing (C1). |
| Boss12Eye | OK | Scope: non-colliding overlay, eight-frame target sampling, angle interpolation and 10-pixel pupil offset. |
| Boss12Arm | OK | Scope: non-colliding lower layer, offsets and sinusoidal rotation. |

## Ships, upgrades and pilots

All chassis numeric fields below match `ShipDefinitions.cs` versus `game_chassis`.
All generator/shield tables pass the importer check. The last column distinguishes
correct definitions from runtime blockers; all purchases also inherit S1.

| Chassis | Definition | Runtime verdict |
| --- | --- | --- |
| Rookie | OK | Not OK: C1/P1. |
| Eagle | OK | Not OK: C1/P1. |
| Crusher | OK | Not OK: C1/P1. |
| Interceptor | OK | Not OK: C1/P1. |
| Phoenix | OK | Not OK: C1/P1. |
| Lightning | OK | Not OK: C1/P1. |
| Dragon | OK | Not OK: C1/P1. |
| Demon | OK | Not OK: C1/P1. |
| Ace | OK | Not OK: C1/P1. |

| Generator | Definition | Regeneration behavior |
| --- | --- | --- |
| Standard | OK | OK: capacity/regen rule. |
| Conduction | OK | OK: capacity/regen rule. |
| Battery | OK | OK: capacity/regen rule. |
| Booster | OK | OK: capacity/regen rule. |
| Impulse | OK | OK: capacity/regen rule. |
| Microwave | OK | OK: capacity/regen rule. |
| Vortex | OK | OK: capacity/regen rule. |
| Capacitor | OK | OK: capacity/regen rule. |
| Electron | OK | OK: capacity/regen rule. |
| Kinetic | OK | Not OK: on-contact regen uses native fixed 4 instead of actual legacy collision damage. |
| Magnetic | OK | OK: big-coin energy gain matches `CBigBonus.OnCollide` / `Ship.GetCoin`; normal coins do not trigger it in legacy either. |
| Fusion | OK | OK: capacity/regen rule. |

| Shield | Definition | Recharge behavior |
| --- | --- | --- |
| Light | OK | OK: drain, efficiency and primary+secondary energy reserve rule. |
| Fiber | OK | OK: same shared rule. |
| Electric | OK | OK: same shared rule. |
| Advanced | OK | OK: same shared rule. |
| Micro | OK | OK: same shared rule. |
| Actuator | OK | OK: same shared rule. |
| Power | OK | OK: same shared rule. |
| Tank | OK | OK: same shared rule. |
| Ultimate | OK | OK: same shared rule. |

Recharge approval is limited to the formula; incoming damage inherits C1/P1 and
hostile collision defects. The native shield-hit sprite overlay is also absent:
legacy `Ship.Draw` draws `ShieldHitVisual` during `ShieldHitDisplayFrames`.

| Pilot / upgrade behavior | Verdict | Evidence |
| --- | --- | --- |
| Kazuki starting loadouts / +20% speed | OK | `PilotStartingItems`, `PilotKazuki` versus `menu.c::start_game`, `game.c::update_players`. |
| Rabbit starting loadouts / +3 coin value | OK | Same loadout table and `update_pickups`. |
| Gunthor starting loadouts / +15% damage | OK | Same loadout table, `WeaponDefinitions` damage multiplier versus `weapon.c::spawn`. |
| Mystery | OK | Scope: dormant placeholder is not offered as a playable fourth pilot. |
| Primary/secondary upgrade price tables | OK | Imported level data and cumulative rounded price formula in `equipment.c`. |
| Powerup pickup | Not OK | Legacy upgrades only `WeaponPrimary`; native increments a level shared by primary/focus and clears every weapon group's state, cancelling unrelated charge/toggles. |
| Pilot active abilities | OK | Scope: remain dormant. Legacy `TryEnable` has no gameplay callers; do not add activation/purchases as a parity fix. |
| Shared player movement | Not OK | Legacy applies input force before firing and updates friction before base physics; native fires before applying input and updates friction after position integration. Flame's velocity multiplier therefore affects different input velocity. |

## Stages and environment

`python tools/import_stages.py --check` passes: **19 stages, 3,448 events, all
source fields accounted for**. The data column covers authored events and fields.
Whole-stage verdicts include the enemies/projectiles above and transition behavior.

| Stage | Authored data | Whole-stage verdict |
| --- | --- | --- |
| Stage1 | OK | Not OK: Boss1, C1/P1/S2. |
| Stage2 | OK | Not OK: Boss2, C1/P1/S2. |
| Stage3 | OK | Not OK: Boss3, C1/P1/S2. |
| Stage4 | OK | Not OK: Boss4, C1/P1/S2. |
| Stage5 | OK | Not OK: Boss5, C1/P1/S2. |
| Stage6 | OK | Not OK: Boss6, C1/P1/S2. |
| Stage7 | OK | Not OK: Boss7, C1/P1/S2. |
| Stage8 | OK | Not OK: Boss8, C1/P1/S2. |
| Stage9 | OK | Not OK: Boss9, C1/P1/S2. |
| Stage10 | OK | Not OK: Boss10, C1/P1/S2. |
| Stage11 | OK | Not OK: Boss11, C1/P1/S2. |
| Stage12 | OK | Not OK: Boss12, C1/P1; final rewards/text exist but death/ending presentation differs. |
| BonusStage1 | OK | Not OK: P1/S2. |
| BonusStage2 | OK | Not OK: P1/S2. |
| BonusStage3 | OK | Not OK: P1/S2. |
| BonusStage4 | OK | Not OK: P1/S2. |
| BonusStage5 | OK | Not OK: P1/S2. |
| BonusStage6 | OK | Not OK: P1/S2. |
| BonusStage7 | OK | Not OK: P1/S2. |

| Other content | Verdict | Source comparison |
| --- | --- | --- |
| Movement presets None, IgnoreCamera, Sin, SinHorizontal, Up, Down, Right, Left, ToUp, DownWaitUp, DownWaitDown, DownWait, DownLeft, DownRight | OK | Scope: velocity formulas, transition thresholds and interpolation coefficients match `MoverPresets.cs`/mover classes versus `movement.c`. Entity update/collision ordering is a separate concern. |
| Building1-9, SmallBuilding1-7 | Not OK | Health/drop tables match. Native projectile collision expands building rectangles with square corners even for circular projectiles, unlike legacy circle/AABB checks. Lightning cannot choose buildings as chain targets. |
| Small coin / big coin | Not OK | Values and magnet equations match; native screen confinement uses fixed X=0..960 rather than panned camera bounds, and bottom removal uses 48 rather than legacy 32 margin. |
| Decorations | Not OK | Native rendering buckets depth by sign, discarding relative ordering among distinct offsets; legacy uses the actual depth value. Imported texture/position/rotation values themselves are retained. |
| Scenery/backgrounds | Not OK | `CBlendingGradientBackground` cycles four corner colors; native has static top/bottom colors. Particle RNG and update sequencing are also different. |
| Secret entries/returns | Not OK | S2; native calculates launch direction after snapping to the entry point, whereas legacy `ReturnFromSecret` calculates it from the ship's previous position. Both return paths copy score/health/energy/shield without replacing the main world's equipment; lack of upgrade transfer is not itself a port mismatch. |
| Health bars / HUD | Not OK | Native does not draw the legacy shield-hit overlay or sidekicks; full HUD parity cannot be approved. |
| Shop / sample ships | Not OK | S1. |
| Menus / game-over / clear screens | Not OK | Immediate state changes replace legacy fades, timed reveal and flyout; P1/S2. |
| Original music assets and authored music events | OK | Scope: original tracks retained and music event names/timing fields match imported stage data. |
| Audio behavior | Not OK | Weapon loops/trails/hit mappings differ; native `game_music` has no game-over `Try_Harder` selection, which legacy `CheckGameOverConditions` requests. |
| Saves / progression records | Not OK | Native saves support Continue, but legacy also saves per-stage scores/medals (`World.SetScoreSaveData`); native checkpoint format lacks those records. |
| Asteroids / SpacePlatform / OldStage* / editor-only effects | Not OK for standalone content parity | No corresponding native asteroid/crack/collision implementation. These are not referenced by the 19 active authored stage streams, so this is not a missing active campaign wave. Archived/editor content is outside the current playable campaign. |

## Repair order

1. Restore player death, contact damage/impulse, Spike/FenceBeam special contact,
   and hostile collision dispatch.
2. Restore primary muzzle offsets, seeking/bounce rules, sidekick/charge drawing,
   projectile sprite selection, rotation, trails and impact effects.
3. Restore each boss's coroutine timing, captured aim and special interactions;
   then death and phase effects.
4. Restore stage stock/sample ships, powerup behavior, secret transitions, fades, flyout,
   scenery blending and remaining HUD/audio/progression presentation.

Use narrow headless regression checks derived from these source rules as fixes
land. A passing full-campaign run is useful for stability, but cannot turn these
known differences into an OK verdict. Legacy gameplay execution is not required
to confirm any of the explicit mismatches listed here.

## Repair progress (2026-11-15)

- C1/P1 gameplay repaired: no automatic respawn; health-based reciprocal contact,
  collision impulse and Kinetic regeneration; Spike/FenceBeam special contact;
  120-frame game-over menu delay and Try_Harder music. Full headless suite and
  Windows native build pass. Death/transition presentation remains tracked below.
- C2/W3 hostile dispatch repaired: discrete post-update bullet collisions,
  rounded building intersections, exact hostile laser/cannon boxes, cannon repeat
  hits/recoil, immunity preserves shots, missiles retain targets, weapon-specific
  pellet/missile blocking. Full headless suite and native build pass.
- W1 and projectile motion/visual fixes: primary +20 muzzle offset, per-emitter
  Flame rotation/spin/growth, Blade spin/flip, Missile/DrunkMissile/BeamFocus
  rotation, random Boomerang spin, invisible two-frame Plasma splash with hit
  state, seekable/unclaimed target selection, Lightning building chains and
  bounce position steps. Sidekick ships, BigLaser/BigFlame and shield overlay
  restored. Full headless suite and windowless software render pass.
- W2/W4 particle repairs: generated original particle definitions, alpha/lifetime
  handling, weapon-specific impacts, missile/plasma trails, charge particles,
  beam impact positions, Bomblet expiry and Boomerang terminal effects. MiniShot
  no longer emits laser particles. Importer check, headless suite, focused impact
  checks and software-render check pass.
- E1/B1 repairs: random death drift and camera compensation, MiniSquare scale,
  BigBall/Airship/Pyramid/chunk bursts, boss multi-explosion counts and child/
  projectile death callbacks. Glob retains attachment/grips/dies; BlackHole
  particles and missile exclusion; Teleporter effects/spin/exit/friction; Pyramid
  fragment difficulty scaling. Headless suite, focused death/Glob checks and
  native build pass.
- Boss behavior repairs: Boss3 burst-boundary routines/startup and chunk spin;
  Boss4 retraction polling/effects and cover bursts; Boss5/Boss8 incoming-damage
  threshold handling; Boss6 sweep delay, orbit boost/windup and sphere death;
  Boss7 captured aim and 15-frame small-cannon aim; Boss7/11 patrol pauses;
  Boss8 wind; Boss10 contact guard/recoil/post-bounce steps/trail; Boss11 random
  spin rests/cannon order; Boss12 final spread order. Full headless suite,
  focused boss timing/threshold checks and native build pass.
- Audit correction: Boss4's UpdateMovement coroutine exists but its Fork call is
  commented out. Its stationary body is correct; do not enable dormant movement.
