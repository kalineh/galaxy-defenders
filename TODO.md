# Active Work

## First native playable slice

- Purpose: give the user an early Windows build using original art/music, backed
  by a shared console and fast headless verification; see `PORT_PLAN.md`.
- Constraints: simple C/raylib; no NPM installs; local co-op; network play deferred.
- Current state: XNA preserved; raylib 6.0 vendored; local compiler setup ready.
  Fixed-tick combat simulation and shared console/script runner pass headless
  checks for movement, swept collisions, co-op, death/reset, pool bounds,
  deterministic runs, assertions, and malformed commands.
- [ ] Establish repository-local compiler/raylib build and native window.
- [ ] Implement fixed-tick combat slice and keyboard/gamepad co-op.
- [ ] Connect the existing shared console to the windowed client.
- [ ] Run focused unit/integration checks, invalid-script checks, and visual smoke.
- [ ] Deliver runnable build, controls, limitations, and next parity milestone.
- Acceptance: user can move/shoot, fight enemies, take damage, restart, hear the
  existing music, and join a second player. Headless checks create no window/audio
  device, exercise real simulation, and return nonzero on failure.
- Next action: implement `src/app.c`, stage original sprites/music, and run a
  native graphical smoke check before delivering the build.
- Verified: Git Bash `./test.sh` (unit checks + combat script + expected CLI failures).
