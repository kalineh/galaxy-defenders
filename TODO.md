# Active Work

## Current handoff

The first native combat trial is runnable via `./play.sh` in Git Bash; controls and explicit
parity gaps are in `README.md`. Native build, headless unit/integration checks,
audio loading, co-op combat render, and console render passed. Original HUD
assets/layout are reused; there is no new UI framework. Human playtesting is
needed for feel and physical controller behavior. Do not use computer-use tools.

## Remaining work

- [x] Restore title/new-game/difficulty/pilot, pause/retry/options navigation and title/pause music.
- [ ] Complete shop/progression menus, saved Continue, original font, transitions,
  sample ships, pilot loadouts and abilities.
- [ ] Add testable console stage selection, money, equipment/upgrades, and
  invincibility commands with validated arguments and observable state.
- [ ] Replace generated trial waves with original authored content, porting the
  required enemy/movement/boss contracts without substituting generic behaviors.
- [ ] Maintain an explicit content parity inventory; do not claim completeness
  from importing stage names or assets alone.

Purpose: the prototype works; the user now requires a faithful port of the
complete existing game. New labels, invented effects, and replacement gameplay
are not acceptable. Preserve legacy source as the behavioral specification.
Current findings: original effects are `ParticleEffect.cs` +
`ParticleEffectDefinitions.cs`, not most of the unused `AnimationEffects.cs`.
Enemy death emits two 24-triangle bursts; player armor damage 8, death 24.
The active lifetime formula halves the nominal lifetime; preserve it for parity.
Normal launch volume is now audible; only automated runs use 1%.
Next action: port original stage/enemy contracts and remaining weapons/loadouts.
Do not disguise trial waves as authored stages. Front Laser normal/focus levels,
Standard Generator energy, focus friction, Kazuki speed and Gunthor damage are
now ported; `player.upgrade SLOT LEVEL` sets zero-based levels 0–7.
Completed: money set, invincibility, healing, chassis listing/equipment commands;
all nine original chassis sprites and base armor/movement values. See
`CONTENT_PARITY.md` for the remaining behavior contracts.
Menu navigation is headless-testable through `menu.open` / `menu.input`.
Title/pilot software captures and a quiet native menu/audio launch passed.

Verification: Git Bash `./test.sh`, `./build.sh native`; native `--headless
--script scripts/checks/combat.txt`; bounded `--frames`/`--capture` and `--console`
runs; `scripts/check_render.ps1` for silent, windowless software rendering.
Scratch is always `artifacts/native-port/`; never store durable inputs there.
