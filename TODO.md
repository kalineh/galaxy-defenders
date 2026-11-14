# Active Work

## Current handoff

The first native combat trial is runnable via `play.bat`; controls and explicit
parity gaps are in `README.md`. Native build, headless unit/integration checks,
audio loading, co-op combat render, and console render passed. Original HUD
assets/layout are reused; there is no new UI framework. Human playtesting is
needed for feel and physical controller behavior. Do not use computer-use tools.

## Remaining work

- [ ] Add a bounded raylib memory/software-render target for windowless captures.
- [ ] Incorporate first-playtest feedback before expanding the port.
- [ ] Audit/port the first authored stage and its actual enemy/boss contracts;
  preserve original music. Follow the milestones in `PORT_PLAN.md`.

Next action: prove one software-rendered capture with the existing vendored
raylib, using fixed output directories and no audio device.

Verification: Git Bash `./test.sh`, `./build.sh native`; native `--headless
--script scripts/checks/combat.txt`; bounded `--frames`/`--capture` and `--console`
runs. Scratch is always `artifacts/native-port/`; never store durable inputs there.
