# Galaxy Defenders — Native Port

A plain C/raylib port of the original XNA two-player arcade shooter.
The original source, content, and credits are preserved in [`legacy/`](legacy/README.md).
Work is on `native`; see [the port plan](PORT_PLAN.md) and [current work](TODO.md).

## Play the first pass

Run `./play.sh` in Git Bash after building. It starts at 30% master volume. The game
opens at the original title menu. Choose Solo Game or Coop Game, New Game,
difficulty, then pilots to enter the original authored Stage 1. The carrier
layout, enemy waves, music change and Boss1 now follow the legacy stage data. Continue is disabled until native saves are implemented.

Menus use arrows/Enter/Escape or gamepad D-pad/A/B. During pilot selection,
player two uses J/L, right Ctrl to confirm, and Backspace to cancel (or gamepad
two). Difficulty applies the original starting money and enemy health scaling;
pilots receive their original chassis. Their complete weapon/shield loadouts
and abilities are still pending. Title, combat, and pause use their original
music tracks. Options adjust SFX/music volume and screen size for this session.
Pause-menu Quit currently returns to the title; its original shop destination
is pending.
The Stage Clear screen now accepts Enter or either gamepad's A/B to
Continue back to the title. It does not advance into an authored second stage;
the original results/shop/stage progression is still pending.

Starting-loadout audit against `legacy/Pilot.cs`:

| Pilot | Original primary (Normal / Hard / Really Hard) | Original secondary (same order) |
| --- | --- | --- |
| Kazuki | Front Laser level 1 / 1 / 2: two / two / four shots | Vulcan level 1 / 2 / 2 |
| Rabbit | Lightning level 2 / 2 / 3 | SeekBomb level 0 / 1 / 1 |
| Gunthor | Front Laser level 2 / 2 / 2: four shots, +15% damage | Missile level 0 / 1 / 2 |

Levels above use the source's zero-based numbering. Native Kazuki/Gunthor
primary patterns match this table; Rabbit still temporarily uses Front Laser.
All secondary weapons and shields are pending. Really Hard should also start
with a Conduction Generator; native currently uses Standard Generator for all.
These missing systems account for the incomplete energy/loadout behavior.
`game.reset` is a debug stage reset (Rookie, level 2 Front Laser), not pilot-based
new-game setup. Boss1 now has its paired bursts, aimed sweeps, smooth lateral
movement and independently destructible side guns. Remaining stages, rewards,
loadouts and progression are still incomplete; see CONTENT_PARITY.md.

| Action | Player 1 | Player 2 |
| --- | --- | --- |
| Move | WASD or arrows | IJKL |
| Fire | Space or Z | Enter or right Ctrl |
| Focus fire / slow movement | Left Shift | Right Shift |
| Join | Joined at launch | F2 |

Gamepad 1 controls player 1; gamepad 2 controls player 2 (Start joins). Use the
left stick/D-pad to move, A/right trigger to fire, LB to focus, and Start to pause.
Keyboard remains available when a controller disconnects. Physical controller
mapping/disconnect behavior still needs human playtesting.

`P`/Escape pauses, `R` restarts, `M` toggles mute, and backtick opens the console.
Shift+backtick expands it; Escape closes it. The console uses Consolas from
Windows (Courier New/bundled Noto Sans Mono fallback), without redistributing system fonts.
All game UI uses bundled Noto Sans. Missing required fonts cause an explicit
load error; raylib's built-in font is never used for game or console text.
`help` lists every command, `help player.` filters, `help player.upgrade` gives
usage, and `help fields` lists every assertion field. Tab completes command
names and positional arguments; Tab again lists ambiguous choices. Up/Down
browse 32 commands and restore your draft. Left/Right/Home/End, Backspace/Delete,
and Ctrl+V edit input. PgUp/PgDn or the mouse wheel scroll 256 output lines;
`clear` clears output. Close the window to exit. Gamepad Y restarts after win/loss.
F3 (or `perf on` / `perf off`) toggles the reference-style top-right frame graph.
Gold is total frame time including pacing/present; cyan is CPU update/draw work
before present, not a GPU timer. The scale is fixed at 0..33.33 ms with a 16.67 ms
guide and 240 samples. F2 remains player-two join.

The port is incomplete. Authored Stage 1 is playable; other main and bonus
stages are not yet enabled. Front Laser has its original eight normal/focus
levels and Standard Generator energy costs/regeneration. Other weapons,
shields, abilities, pickups/rewards, saves, shop and campaign progression remain
pending. Inactive HUD meters stay empty. Web/mobile builds are also pending.
See CONTENT_PARITY.md for specific gaps.

## Build and test (Windows)

Install Git for Windows, then run these commands from the repository in PowerShell:

```powershell
.\scripts\setup.ps1
& 'C:\Program Files\Git\bin\bash.exe' ./build.sh headless
& 'C:\Program Files\Git\bin\bash.exe' ./build.sh native
& 'C:\Program Files\Git\bin\bash.exe' ./test.sh
.\scripts\check_render.ps1
```

Setup obtains checksum-verified Zig 0.13.0 in ignored `external/zig/`. Raylib 6.0
source is vendored. No other checkout, NPM, global compiler, or XNA installation is
required. Build outputs reuse `build/headless/` and `build/native/`.

The headless runner runs the same fixed-tick simulation as the client,
without linking raylib,
opening a window, initializing audio, or waiting for wall-clock time.

Run `build/native/galaxy.exe` directly, or copy the entire `build/native/` folder
to share it. Assets are resolved beside the executable, independently of the
working directory. `--volume 0.3` selects a louder master level; `--mute` skips
audio initialization entirely. Direct interactive launches default to 30%;
bounded `--frames` checks default to 1%.

## Scripted checks

```powershell
.\build\headless\galaxy.exe --script scripts\checks\combat.txt
.\build\headless\galaxy.exe --command 'game.reset 42;game.waves 0;player.join 2;game.step 60;state'
.\build\headless\galaxy.exe --command help
```

Scripts contain one command per line; semicolon chains, blank lines, and `#`
comments are supported. `player.input SLOT X Y FIRE FOCUS` holds player actions
until changed. `game.step N` advances exact 60 Hz ticks. Assertions use fields
such as `p1.health`, `kills`, and `tick`; example: `assert p1.health eq 20`.
Commands are bounded to 72,000 requested ticks per process and 4,096 script lines.
Debug equipment commands: `chassis.list`, `player.chassis 1 Demon`,
`player.money 1 25000` (sets the balance), `player.invincible 1 1` (0 disables),
and `player.heal 1`. Chassis changes preserve the current armor percentage and
use the original sprite, armor, acceleration, and friction. They cost no money.
`player.upgrade 1 7` sets Front Laser's level (zero-based 0–7, matching the legacy
data). It changes both normal and focus patterns without charging money. Inspect
`p1.weapon_level` and `p1.energy` through assertions or `state`.
`game.stage 1 [PROGRESS]` seeks Stage 1, with normalized progress from 0 to 1.
It replays nominal stage time (0 = start; 0.9 = boss section), rebuilding enemy
positions, bullets, camera stop and music events. The camera waits at the boss,
so progress 1 does not kill it. Equipment/money survive; armor is restored.
Music seeks to the current track's elapsed time, including the boss-track switch.
Other stage IDs fail explicitly until their behavior is ported. Importing their
source data does not make those stages playable.

The shop and currency rewards are still pending. `state` reports these values;
assertions also support `p1.money`, `p1.chassis`, `p1.armor`, `p1.invincible`
and their `p2` equivalents. See [content parity](CONTENT_PARITY.md).
`menu.open main` or `menu.open pause` opens a menu; `menu.input
up/down/left/right/confirm/back [SLOT]` drives the same navigation as the client.
Use a script or command at launch to bypass the title for combat debugging.
Unknown commands, invalid arguments, and failed assertions exit nonzero. The
test runner intentionally exercises failures, so expected `ERROR`/`FAIL` lines
appear before its final PASS result.

The native executable also supports `--headless` with the same commands. For a
bounded render check, use `--frames 120 --capture artifacts/native-port/combat.png`.
This mode holds scripted player input, advances exactly one tick per frame, and
exits; `--console --frames 1` captures the console without desktop automation.
Create the capture directory first. Screenshots are supporting evidence, not a
pixel-parity gate. Automated tests use no computer-use tools.

Prefer `scripts/check_render.ps1` for visual smoke checks: it builds
`build/headless/galaxy-render.exe` against raylib's memory/software backend,
renders a fixed scene, and checks sprite color, channel order, and orientation.
It creates no window or audio device, requires no GPU, and overwrites the same
scratch image/log each run. Use `./build.sh render` and then
`galaxy-render.exe --script PATH --frames 1 --capture PATH` for other scenes.
Unlike native interactive rendering, this target draws directly into a memory
framebuffer. Physical input and audible playback remain human playtest checks.

## Scratch data

Everything in `artifacts/` is disposable and may be deleted at any time. This
task reuses `artifacts/native-port/` for logs/captures, never durable files.
`scripts/clean_artifacts.ps1` previews only; `-Apply` opts into guarded cleanup
of that exact marked folder. It refuses links/junctions and never targets source,
saves, compiler files, or another task's directory.
