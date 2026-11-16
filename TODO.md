# Active Work

## Current request and handoff

Thread: `01a1db6e-6640-75d3-aed6-1b8301bb3878`.
User requested all weapons, enemies, stages and upgrades, with original behavior.
Use headless CLI checks; never launch visible PowerShell windows or use desktop
automation. No npm installs. Commit and push verified focused changes to `native`.

## Implemented

- Original title/combat/pause music, including Stage 1 startup and seek synchronization.
- All weapon definitions and level tables, primary/support/sidekick firing,
  generators, shields, original pilot loadouts, six working shop categories.
- All regular enemies and Boss1-Boss12 combat, component damage and phase changes.
- All 12 main and seven bonus stage event streams, original required assets,
  scenery layers, music changes, camera stops, clear/shop/next-stage progression.
- Secret entry/return retains the main encounter and transfers bonus rewards.
- Original Stage12 ending text/music and victory money.
- Console equipment/spawn/seek commands, completion/history and F3 frame graph.

## Remaining parity work

- Audit every projectile's collision, special effects and sound against legacy;
  finish pickups, original coin/score awards, salvage and player death behavior.
- Audit boss attack timing/movement in detail (current coverage checks all phases,
  shared parts, death cleanup and authored spawn/clear, not frame-identical traces).
- Native saves/Continue and replay stage selection after winning.
- Original chassis/pilot abilities, sample ships, shop inventory restrictions,
  menu transitions/fades, stage-clear explosion/flyout presentation.
- Validate secret entrance reachability at arena edges; preserve source coordinates.
  Secret return/fade timing and scrolling need closer legacy comparison.
- Blending gradient scenery, remaining boss visual details, turret corpses.
- Web/phone builds and physical controller playtests remain outside verified Windows.

## Verification

`./test.sh`: all 19 stages run from start through clear; no dropped spawns after
increasing enemy capacity for BonusStage6. All 12 bosses are encountered; checks
kill later bosses after 600 ticks to validate progression, not difficulty balance.
Tests cover shop/loadouts, phase attacks, collision boundaries and secret state
roundtrip. `./build.sh native` and `scripts/check_render.ps1` pass. All 19 stages
have windowless software captures in `artifacts/native-port/stage-N.png`.
Legacy stage importer `--check` verifies 3,448 events without omitted source fields.

See CONTENT_PARITY.md for current scope and limitations. Scratch artifacts are
transient; source and required assets stay in the repository.
