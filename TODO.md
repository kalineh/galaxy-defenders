# Active Work

## Current handoff

The first native combat trial is runnable via `play.bat`; controls and explicit
parity gaps are in `README.md`. Native build, headless unit/integration checks,
audio loading, co-op combat render, and console render passed. Original HUD
assets/layout are reused; there is no new UI framework. Human playtesting is
needed for feel and physical controller behavior. Do not use computer-use tools.

## Remaining work

- [ ] Incorporate first-playtest feedback before expanding the port.
- [ ] Audit/port the first authored stage and its actual enemy/boss contracts;
  preserve original music. Follow the milestones in `PORT_PLAN.md`.

Next action: collect first-playtest feedback, then inventory Stage1's actual
spawn/scenery/boss definitions before replacing the simplified combat trial.

Verification: Git Bash `./test.sh`, `./build.sh native`; native `--headless
--script scripts/checks/combat.txt`; bounded `--frames`/`--capture` and `--console`
runs; `scripts/check_render.ps1` for silent, windowless software rendering.
Scratch is always `artifacts/native-port/`; never store durable inputs there.
