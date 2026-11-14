# Repository Guidelines

## Direction & Scope

Port Galaxy Defenders from XNA/C# to simple C with raylib, targeting PC, phones, and web. Preserve the arcade shooter gameplay, two-player local co-op, and keyboard/gamepad support. Defer network multiplayer. Music will be reworked; choose its synthesis approach separately. Human direction owns scope; avoid speculative engine features.

## Project Structure & References

The original XNA game is preserved in `legacy/`: `galaxy.sln` and `galaxy.csproj` define it. Gameplay lives in its `Ship/`, `Enemy/`, `Weapon/`, `Projectile/`, and `Stage/` directories; authored stages in `StageDefinitions/`, assets in `Content/`, and presentation in `Render/`, `Hud/`, and `Menu/`.

Keep the archive's relative paths and attribution intact. Develop the native game at the repository root: `src/` for coarse gameplay modules, `assets/` for runtime content, `tools/` for offline utilities, and ignored `build/` for outputs. Create directories when needed.

Read siblings as optional references, never build/runtime dependencies. They may
not exist on another machine; keep all required code/assets and setup instructions here:

- `../solcraft`: architecture, raylib, input, and audio.
- `../mmoa`: renderer, web integration, and profiling.
- `../dragonwake`: agent workflow and synthesis/audio tooling.

## Build & Development Commands

No native build scripts exist yet. Establish simple `build.sh`, `build_web.sh`, and phone-target scripts as those targets become runnable; document exact commands in `README.md`.

On Windows, invoke shell scripts through Git Bash, for example `& 'C:\Program Files\Git\bin\bash.exe' ./build.sh`, once available. Use PowerShell for ad-hoc commands and `rg` for searches. Do not install NPM packages without explicit approval of the exact packages.

## Coding Style

Use four-space indentation, `snake_case` functions/variables, and explicit constant prefixes. Prefer plain structs, free functions, small headers, and visible `*_init`, `*_update`, `*_draw`, and `*_shutdown` lifetimes. Separate gameplay state from presentation. Avoid class hierarchies, generic frameworks, unnecessary dependencies, and per-frame allocation. No formatter or linter is configured yet.

## Testing Guidelines

Use the same console commands for interactive debugging and scripted headless checks. Test actual gameplay without a window/audio device; use fixed ticks and explicit seeds. Prioritize movement, collisions, progression, co-op, and controller disconnects. Build before dependent checks. Deliver playable builds for human feedback; screenshot comparisons are optional. See `PORT_PLAN.md`. Report untested platforms honestly.

Do not use desktop/computer-use automation for this project. Verify through CLI
scripts, headless tests, and programmatic render captures; humans own interactive
playtesting. Automated graphical checks default to 1% master volume; headless
checks never initialize audio. Follow the legacy HUD and reference console/UI
patterns rather than inventing a new UI system or redesigning the game.

## Disposable Artifacts

`artifacts/` contains only transient files that may be deleted at any time: logs,
captures, scratch experiments, and disposable reports. Never store source, assets,
saves, dependencies, or required build inputs there. Reuse `artifacts/native-port/`
for this task and fixed `build/native/` and `build/headless/` output directories;
do not create a fresh build tree per check. `scripts/clean_artifacts.ps1` previews
cleanup by default; `-Apply` explicitly deletes only owned task scratch contents.

## Agent, Commit & Review Workflow

Work on `native`. Keep unfinished multi-step work in `TODO.md`. Commit and push each verified, focused piece before starting another; preserve unrelated edits. History uses short imperative subjects, such as `Fix bug in update logic.` Follow that convention. Review descriptions should explain behavior, validation, linked issues when applicable, and screenshots for visual changes. Never commit builds, caches, or logs. For documentation-only changes, inspect the diff.
