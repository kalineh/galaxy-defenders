# Content parity

The legacy C# source is the specification. Asset imports alone do not mean a
system or stage is ported. The native build still contains a combat trial.

| Area | Native state | Source / remaining work |
| --- | --- | --- |
| HUD | Original panels, portraits, colors; prototype text removed | `Hud/`: pilot states, meters, fonts, abilities pending |
| Particles | Armor hit, player death, focus, enemy death and shot hit | `Effects/ParticleEffectDefinitions.cs`; remaining weapon/stage effects pending |
| Audio | Original combat music and core hit/death sounds | `Audio.cs`; title/stage music selection pending |
| Chassis | All nine sprites and armor/speed/friction values; console equipment | `Ship/ShipDefinitions.cs`; focus interpolation, collision resistance and pilot modifiers pending |
| Debug | Money set, invincibility, healing, chassis, spawning, fixed ticks | Weapon upgrades and authored stage jumping pending |
| Weapons | Trial twin shot only | `Weapon/`, `Projectile/`; original primary, focus, secondary, sidekick systems pending |
| Menus | Pending | `State/StateMainMenu.cs`, `OptionsMenu.cs`, `Hud/HudPilotSelect.cs`, `PauseManager.cs` |
| Economy | Per-player money state only | Shop, rewards, salvage, saves pending |
| Stages | Generated trial only | Active Stage1–12 and BonusStage1–7 definitions pending; `OldStage*` are references, not active content |
| Enemies/bosses | Four simplified trial behaviors | `Enemy/` and stage behavior contracts pending |
| Platforms | Windows native and silent software checks | Web, phone, physical controller testing pending |

Do not expose an authored stage name while running trial waves underneath it.
Stage selection must dispatch the actual authored content as it becomes available.
