# Active Work

## Updated required scope

User explicitly rejected the trial/placeholder approach. Remove generated trial
waves and substitute bosses; port all original authored stages, enemies, bosses,
weapons/loadouts, shop, upgrades, menus, saves and progression one-to-one.
Do not present partial content as complete or rename trial waves as Stage 1.
Add `game.stage STAGE [0..1]` seeking authored state and synchronizing music.
Replace all raylib built-in text (completed with licensed bundled Noto fonts).
Also add the reference lightweight top-right frame-time graph/function key.
Completed graph: F3 / `perf [on|off]`, 240 frame/work samples, top-right,
fixed 16.67 ms midpoint, clean font. F2 remains co-op joining.
Keep small verified commits/pushes, self-contained assets, CLI-only checks.

## Current handoff

Console follow-up: matched Solcraft's Windows fixed-font selection (Consolas)
and MMOA's help/completion pattern. Shared command catalog now supplies complete
help, usage errors and positional completion, including chassis and assertion
fields. Added cursor editing, 32-command history/draft restoration, 256-line
scrollback, and headless editor checks. No sibling runtime dependencies.

Playtest follow-up: Stage Clear now opens a shared menu state; Enter or either
gamepad A/B returns to title. This fixes the trial dead end, not campaign
progression. Verified full new-game restart after clearing, plus effects ending.
README has the exact starting-loadout audit from `legacy/Pilot.cs`; Rabbit's
Lightning, all secondaries/shields, and Really Hard's Conduction Generator remain
missing. The boss is still a trial substitute: original Boss1 has paired bursts,
aimed sweeps, destructible side guns, and different movement/health.

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
