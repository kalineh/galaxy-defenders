# Repository Guidelines

## Direction & Scope

Port Galaxy Defenders from XNA/C# to simple C with raylib, targeting PC, phones, and web. Preserve the arcade shooter gameplay, two-player local co-op, and keyboard/gamepad support. Defer network multiplayer. Music will be reworked; choose its synthesis approach separately. Human direction owns scope; avoid speculative engine features.

## Project Structure & References

Currently, `galaxy.sln` and `galaxy.csproj` define the XNA game. Gameplay lives in `Ship/`, `Enemy/`, `Weapon/`, `Projectile/`, and `Stage/`; authored stages live in `StageDefinitions/`, assets in `Content/`, and presentation in `Render/`, `Hud/`, and `Menu/`.

At port initialization, move the original project and assets intact into `legacy/`, preserving relative paths and attribution. Develop the native game at the repository root: `src/` for coarse gameplay modules, `assets/` for runtime content, `tools/` for offline utilities, and ignored `build/` for outputs. Create directories when needed.

Read siblings as references, never dependencies:

- `../solcraft`: architecture, raylib, input, and audio.
- `../mmoa`: renderer, web integration, and profiling.
- `../dragonwake`: agent workflow and synthesis/audio tooling.

## Build & Development Commands

No native build scripts exist yet. Establish simple `build.sh`, `build_web.sh`, and phone-target scripts as those targets become runnable; document exact commands in `README.md`.

On Windows, invoke shell scripts through Git Bash, for example `& 'C:\Program Files\Git\bin\bash.exe' ./build.sh`, once available. Use PowerShell for ad-hoc commands and `rg` for searches. Do not install NPM packages without explicit approval of the exact packages.

## Coding Style

Use four-space indentation, `snake_case` functions/variables, and explicit constant prefixes. Prefer plain structs, free functions, small headers, and visible `*_init`, `*_update`, `*_draw`, and `*_shutdown` lifetimes. Separate gameplay state from presentation. Avoid class hierarchies, generic frameworks, unnecessary dependencies, and per-frame allocation. No formatter or linter is configured yet.

## Testing Guidelines

No automated framework or coverage threshold exists. Build native debug first, then check affected gameplay. Prioritize movement, collisions, stage progression, two-player input, and controller disconnects. Add focused deterministic regression checks when useful; use `test_<subsystem>.c` for standalone tests. Check visual changes in-game. Validate web/phone targets when affected; report untested platforms honestly.

## Agent, Commit & Review Workflow

Work on `native`. Keep unfinished multi-step work in `TODO.md`. Commit each verified, focused change before starting another; preserve unrelated edits. History uses short imperative subjects, such as `Fix bug in update logic.` Follow that convention. Review descriptions should explain behavior, validation, linked issues when applicable, and screenshots for visual changes. Never commit builds, caches, or logs. For documentation-only changes, inspect the diff.
