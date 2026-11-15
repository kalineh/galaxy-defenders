# Content parity

The legacy C# source is the specification. Asset imports alone do not mean a
system or stage is ported. The native build runs authored Stage 1; the complete game port remains unfinished.

| Area | Native state | Source / remaining work |
| --- | --- | --- |
| HUD | Original panels, portraits, colors; prototype text removed | `Hud/`: pilot states, meters, fonts, abilities pending |
| Particles | Armor hit, player death, focus, enemy death and shot hit | `Effects/ParticleEffectDefinitions.cs`; remaining weapon/stage effects pending |
| Audio | Original title/combat/pause tracks, menu and core hit/death sounds | `Sound.cs`; Stage 1 boss music switch/seek added; other stages pending |
| Chassis | All nine sprites and armor/speed/friction values; focus friction interpolation, Kazuki speed bonus; console equipment | `Ship/ShipDefinitions.cs`; collision resistance and abilities pending |
| Debug | Money set, invincibility, healing, chassis, Front Laser levels, spawning, fixed ticks, menu navigation | `game.stage 1 [progress]` rebuilds timeline and seeks music; other stages/equipment pending |
| Weapons | Front Laser's eight normal/focus levels, offsets, damage, speed, reload, hit particles, audio; Gunthor damage bonus | `Weapon/`, `Projectile/`; other primary, secondary and sidekick weapons pending; collisions still use prototype swept circles |
| Energy | Standard Generator capacity/regeneration and Front Laser consumption, active HUD meter | Other generators and shield drain/regeneration pending |
| Menus | Title, new-game, difficulty, pilot, pause/retry/quit confirmation, options, game-over navigation and original textures | `State/StateMainMenu.cs`, `OptionsMenu.cs`, `Hud/HudPilotSelect.cs`, `World.cs`; Continue/saves, shop destination, fades, sample ships and Pericles font pending |
| Pilots/difficulty | Original names, portraits, unique co-op selection, starting chassis/money, enemy health scaling, Kazuki speed/Gunthor damage | Complete weapon/generator/shield loadouts, abilities and money scaling pending; Rabbit still uses Front Laser until Lightning is ported |
| Economy | Per-player money state only | Shop, rewards, salvage, saves pending |
| Stages | Stage 1 authored events/movers/carrier scenery/buildings/music/camera stop; all 19 stage tables imported | Stage2?12 and BonusStage1?7 runtime contracts remain pending; OldStage files are references only |
| Stage clear | Continue now returns to title, with keyboard and either gamepad | Authored camera stop waits for all enemies, then 220 ticks; original results, rewards, shop and next-stage progression remain pending |
| Enemies/bosses | Stage 1 enemy types and Boss1 paired bursts, aimed sweep, side guns and smooth movement | `Enemy/` and stage behavior contracts pending |
| Platforms | Windows native and silent software checks | Web, phone, physical controller testing pending |

Generated trial waves and substitute boss behavior have been removed.
Stage 1 still has parity gaps: pickups/rewards, turret corpses, stage-clear flyout/fades, randomized cloud initialization,
complete starting loadouts, player damage/death rules, and exact collision order.
Other stages are explicitly unavailable rather than using substitute enemies.

Collision audit: EnemyShot renders its native 40x40 texture (previously shrunk
by a hard-coded 24-pixel width). Its hit radius is 2; the unshielded player
radius is 16. Circle pairs preserve legacy `r1*r1 + r2*r2` distance threshold.
Boss1 side guns use 120x120 boxes. Native bullets still use swept tests between
ticks; primary laser collision geometry and shields remain separate parity work.
