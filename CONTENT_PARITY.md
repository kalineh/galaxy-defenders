# Content parity

The legacy C# source is the specification. Asset imports alone do not mean a
system or stage is ported. The native build still contains a combat trial.

| Area | Native state | Source / remaining work |
| --- | --- | --- |
| HUD | Original panels, portraits, colors; prototype text removed | `Hud/`: pilot states, meters, fonts, abilities pending |
| Particles | Armor hit, player death, focus, enemy death and shot hit | `Effects/ParticleEffectDefinitions.cs`; remaining weapon/stage effects pending |
| Audio | Original title/combat/pause tracks, menu and core hit/death sounds | `Sound.cs`; authored stage music selection pending |
| Chassis | All nine sprites and armor/speed/friction values; focus friction interpolation, Kazuki speed bonus; console equipment | `Ship/ShipDefinitions.cs`; collision resistance and abilities pending |
| Debug | Money set, invincibility, healing, chassis, Front Laser levels, spawning, fixed ticks, menu navigation | Authored stage jumping and remaining equipment commands pending |
| Weapons | Front Laser's eight normal/focus levels, offsets, damage, speed, reload, hit particles, audio; Gunthor damage bonus | `Weapon/`, `Projectile/`; other primary, secondary and sidekick weapons pending; collisions still use prototype swept circles |
| Energy | Standard Generator capacity/regeneration and Front Laser consumption, active HUD meter | Other generators and shield drain/regeneration pending |
| Menus | Title, new-game, difficulty, pilot, pause/retry/quit confirmation, options, game-over navigation and original textures | `State/StateMainMenu.cs`, `OptionsMenu.cs`, `Hud/HudPilotSelect.cs`, `World.cs`; Continue/saves, shop destination, fades, sample ships and Pericles font pending |
| Pilots/difficulty | Original names, portraits, unique co-op selection, starting chassis/money, enemy health scaling, Kazuki speed/Gunthor damage | Complete weapon/generator/shield loadouts, abilities and money scaling pending; Rabbit still uses Front Laser until Lightning is ported |
| Economy | Per-player money state only | Shop, rewards, salvage, saves pending |
| Stages | Generated trial only | Active Stage1–12 and BonusStage1–7 definitions pending; `OldStage*` are references, not active content |
| Stage clear | Continue now returns to title, with keyboard and either gamepad | Original results, rewards, shop and next-stage progression remain pending |
| Enemies/bosses | Four simplified trial behaviors | `Enemy/` and stage behavior contracts pending |
| Platforms | Windows native and silent software checks | Web, phone, physical controller testing pending |

Do not expose an authored stage name while running trial waves underneath it.
Stage selection must dispatch the actual authored content as it becomes available.
