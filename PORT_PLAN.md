# Native Port Plan

## Direction

Port the XNA game to plain C and raylib on branch `master`. Ship small playable
increments for human feedback. Preserve two-player local co-op and gamepad support;
network multiplayer is deferred. Use the original music now; replacement music and
synthesis are later work. PC, web, and phones are the eventual targets; Windows is
the first playable target. Phone OS and touch-control design remain open.

## Architecture

- Preserve the XNA source and assets intact in `legacy/`.
- Keep native source in `src/`, runtime assets in `assets/`, scripts in `scripts/`,
  and generated outputs in ignored `build/`.
- Use plain structs, explicit ownership, fixed-capacity entity/projectile pools,
  and a few cohesive modules. No ECS, scripting language, or generic engine.
- Run gameplay at a fixed tick. Inspect original frame-based movement, firing,
  spawn timing, and collision rules before claiming parity.
- Feed player actions into the same simulation from keyboard, gamepads, and test
  scripts. Rendering and audio consume state/events; simulation needs neither a
  window nor an audio device.
- Translate behavior rather than reproducing the C# inheritance tree. Keep
  approximations in early playable slices explicitly documented.

## Console and Verification

Build a small shared command dispatcher used by the in-game console, launch
arguments, and line-oriented script files. Support seeded reset, player actions,
fixed-tick advance, state inspection, assertions, and exit. Unknown commands,
invalid arguments, failed assertions, and exhausted execution limits must fail
with a nonzero exit code and a useful script line number. Avoid a remote-control
server until a real task needs one.

The fastest checks run the actual gameplay with drawing/audio disabled and no
real-time sleeps. Add focused C unit checks for boundary cases and command scripts
for integration flows. Test collision, damage, cooldowns, pool limits, death,
restart, stage transitions, and co-op independently; then exercise their integration.
Passing isolated systems is not proof that all content or presentation works.

Raylib 6.0 is vendored. Its [memory platform](https://github.com/raysan5/raylib/blob/6.0/src/platforms/rcore_memory.c)
uses software rendering and has no input system. `./build.sh render` and
`scripts/check_render.ps1` provide windowless, silent render checks. The build
requests RGBA output; screen readback corrects the software backend's top-down
row order. Avoid its stubbed texture readback. No-render simulation remains the
fast default. Exported frames can be inspected, but pixel comparisons against XNA
or GPU output are not a prerequisite.

Deliver a Windows build early. Use human playtests for feel, visuals, music, and
physical controller behavior, with small automated render/input smoke checks.
Keep checks proportional; do not build a screenshot-baseline project first.
Do not use computer-use/desktop automation. Expose any required verification via
the CLI or testable code. Headless checks are silent; bounded graphical runs use
1% master volume by default. Preserve the original HUD assets/layout and follow
the established reference console interaction/rendering pattern; do not invent a
new UI framework or visual redesign.

## Milestones

1. Archive XNA; establish local build, window, fixed-tick simulation, console,
   and headless script checks. Deliver a playable combat slice using original
   sprites and music, with movement, shooting, enemies, damage, restart, and co-op.
2. Audit and port one complete authored stage, its enemy patterns and boss,
   scoring/progression, and required menus. Compare behavior against legacy code.
3. Prove the small game on web and the chosen phone target before bulk content
   conversion. Check packaging, audio activation, input, and lifecycle behavior.
4. Port remaining stages, bosses, weapons, upgrades, effects, and persistence
   in independently verified slices. Maintain a concrete parity checklist.
5. Tune presentation and touch controls from playtests. Revisit music separately.

## Reference Map

Read-only references: `../solcraft` for raylib/console/input architecture,
`../dragonwake/SERVER.md` for one-shot scripts and failures,
`../mmoa` for web/rendering/profiling, and `../../infinity-rpg/NATIVE_WORKFLOW.md`
for focused subsystem verification. Adapt principles; do not import their large
frameworks or depend on sibling files at build/runtime.

## Delivery Rules

Use simple build scripts and explicit commands. Commit each verified piece.
Keep the current execution queue in `TODO.md`; record durable decisions here.
Never imply proposed commands or deferred features already work.

All sibling references are optional: builds and runtime must work without them.
Commit and push small verified pieces to `native` throughout implementation.
Reuse fixed build directories and `artifacts/native-port/` for this task. Everything
under `artifacts/` is transient and may be deleted at any time; never keep durable
inputs or saves there. Cleanup previews by default, requires an ownership marker,
rejects links/junctions, and has no caller-supplied target path.
