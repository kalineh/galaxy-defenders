# Active Work

## First native playable slice

- Purpose: give the user an early Windows build using original art/music, backed
  by a shared console and fast headless verification; see `PORT_PLAN.md`.
- Constraints: simple C/raylib; no NPM installs; local co-op; network play deferred.
- Current state: XNA archived intact into `legacy/`; all 727 file hashes match the
  original commit. Plan and safe artifact workflow pushed to `native`.
- [ ] Establish repository-local compiler/raylib build and native window.
- [ ] Implement fixed-tick combat slice and keyboard/gamepad co-op.
- [ ] Share console commands across windowed and headless script execution.
- [ ] Run focused unit/integration checks, invalid-script checks, and visual smoke.
- [ ] Deliver runnable build, controls, limitations, and next parity milestone.
- Acceptance: user can move/shoot, fight enemies, take damage, restart, hear the
  existing music, and join a second player. Headless checks create no window/audio
  device, exercise real simulation, and return nonzero on failure.
- Next action: establish the self-contained native build and window.
- Exact verification commands will be recorded when the build interface exists.
