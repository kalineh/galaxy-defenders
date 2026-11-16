# Active Work

## Current request and handoff

Thread: `01a1db6e-6640-75d3-aed6-1b8301bb3878`.
User requested all weapons, enemies, stages and upgrades, with original behavior.
Use headless CLI checks; never launch visible PowerShell windows or use desktop
automation. No npm installs. Commit and push verified focused changes to `native`.

Active request: fix every Not OK item in CONTENT_AUDIT.md. Work through shared
gameplay/collision defects, projectile visuals/effects, boss behavior/effects,
then shop/progression/UI/scenery and standalone archived content. Verify focused
pieces and commit/push each to native. Do not stop after the first batch.
Current batch: archived stages verified (11 playable, OldStage4 source defect); final effect review and audit.
Completed/pushed: death/contact, collisions, weapon motion/targeting/sprites,
authored particles, enemy/boss behavior/effects, player/pickups, scenery,
release shop stock (2d25a50), persistent seed (47643c7), missile effects (e3d1fca).
Remaining: final effect review and audit reconciliation. Pending user input:
OldStage4 building at (325.51, -16555.24) has an empty original TextureName;
asked which original building asset should be used. Do not invent a replacement. Beam looping audio was a false audit finding: Cue fields unused,
firing Sound commented out in legacy; do not add it.
Do not run the legacy game to establish differences explicit in source.

## Implemented

- Original title/combat/pause music, including Stage 1 startup and seek synchronization.
- All weapon definitions and level tables, primary/support/sidekick firing,
  generators, shields, original pilot loadouts, six working shop categories.
- All regular enemies and Boss1-Boss12 combat, component damage and phase changes.
- All 12 main and seven bonus stage event streams, original required assets,
  scenery layers, music changes, camera stops, clear/shop/next-stage progression.
- Boss sprite/geometry audit: corrected body/arm/cover layers, Boss6 center spin,
  Boss9 initial ball offsets/replacement timers, Boss10 block fades and offsets,
  and round-projectile collisions near rectangular corners.
- Regular-enemy sprite/geometry/attachment audit: layered Pyramid/Cutter visuals,
  Spike/FenceBeam frames, correct Pyramid fragments and Cutter/Swirl transforms,
  linked Fence cleanup, stationary turret corpses and Glob/BlackHole layers.
- Secret entry/return retains the main encounter and transfers bonus rewards.
- Original horizontal camera pan and radius-based player bounds; all seven secret
  entrances pass actual entry, bonus playback and return tests.
- Original Stage12 ending text/music and victory money.
- Separate solo/co-op saves, validated atomic persistence, Continue to the correct
  stage/shop and unlocked replay selection after winning. Pause Quit opens shop.
- Legacy chassis resistance on impacts, shield/armor hit feedback and knockback;
  removed the invented immunity interval after ordinary damage.
- Original coin/big-coin/upgrade pickups, magnet behavior, Rabbit coin bonus,
  generator coin recovery, health-based kill/building scores and co-op damage scale.
- Console equipment/spawn/seek commands, completion/history and F3 frame graph.
- Pellet blocking uses laser/beam collision shapes and original spin; rotating
  turrets render both layers and interpolate between volleys; splitter halves use
  distinct sprites, original offsets, inherited velocity and opposite spins.

## Remaining parity work

- Finish the remaining findings in CONTENT_AUDIT.md; historical Not OK rows need
  reconciliation with repair progress, not reimplementation of completed fixes.
- Shop sample firing and departure restored; full suite/render checks pass.
- Stage-clear stats/awards/medals and per-stage save records now restored.
- Secret return timing/camera/direction repaired; full suite covers all seven routes.
- State fades, scenery growth/order and .NET-compatible seeded RNG restored.
- Review archived standalone content and reconcile every original audit row.
- Boss coverage checks phases/shared parts/death/authored spawns, not exhaustive
  frame-identical traces against the legacy runtime.
- Pilot ability classes are dormant in the legacy game: TryEnable has no callers,
  no purchase UI exists, and ability HUD drawing is commented out. Do not invent
  activation or purchasing for those unused definitions.
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

