# Active Work

## First native playable slice

- Purpose: give the user an early Windows build using original art/music, backed
  by a shared console and fast headless verification; see `PORT_PLAN.md`.
- Constraints: simple C/raylib; no NPM installs; local co-op; network play deferred.
- Current state: original XNA source at root; no native implementation yet.
- [ ] Archive XNA intact into `legacy/` in a separate commit.
- [ ] Establish repository-local compiler/raylib build and native window.
- [ ] Implement fixed-tick combat slice and keyboard/gamepad co-op.
- [ ] Share console commands across windowed and headless script execution.
- [ ] Run focused unit/integration checks, invalid-script checks, and visual smoke.
- [ ] Deliver runnable build, controls, limitations, and next parity milestone.
- Acceptance: user can move/shoot, fight enemies, take damage, restart, hear the
  existing music, and join a second player. Headless checks create no window/audio
  device, exercise real simulation, and return nonzero on failure.
- Next action: archive tracked legacy files, retaining root contributor/port docs.
- Exact verification commands will be recorded when the build interface exists.
