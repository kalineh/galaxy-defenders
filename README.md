# Galaxy Defenders — Native Port

A plain C/raylib port of the original XNA two-player arcade shooter.
The original source, content, and credits are preserved in [`legacy/`](legacy/README.md).
Work is on `native`; see [the port plan](PORT_PLAN.md) and [current work](TODO.md).

## Play the first pass

Run `./play.sh` in Git Bash after building. It starts at 30% master volume. The game
opens at the original title menu. Choose Solo Game or Coop Game, New Game,
difficulty, then pilots to enter the combat trial. A simplified boss appears
after 45 seconds. Continue is disabled until native saves are implemented.

Menus use arrows/Enter/Escape or gamepad D-pad/A/B. During pilot selection,
player two uses J/L, right Ctrl to confirm, and Backspace to cancel (or gamepad
two). Difficulty applies the original starting money and enemy health scaling;
pilots receive their original chassis. Their complete weapon/shield loadouts
and abilities are still pending. Title, combat, and pause use their original
music tracks. Options adjust SFX/music volume and screen size for this session.
Pause-menu Quit currently returns to the title; its original shop destination
is pending.

| Action | Player 1 | Player 2 |
| --- | --- | --- |
| Move | WASD or arrows | IJKL |
| Fire | Space or Z | Enter or right Ctrl |
| Focus / slow movement | Left Shift | Right Shift |
| Join | Joined at launch | F2 |

Gamepad 1 controls player 1; gamepad 2 controls player 2 (Start joins). Use the
left stick/D-pad to move, A/right trigger to fire, LB to focus, and Start to pause.
Keyboard remains available when a controller disconnects. Physical controller
mapping/disconnect behavior still needs human playtesting.

`P`/Escape pauses, `R` restarts, `M` toggles mute, and backtick opens the console.
Shift+backtick expands it, Up recalls the last command, Down clears it, and
Escape closes it. Close the window to exit. Gamepad Y restarts after win/loss.

This is a foundation playtest, not a complete stage port. It has movement,
shooting, three enemy types, a trial boss, armor, lives, scoring, and local co-op.
Wave patterns, weapon behavior, and boss attacks are simplified. Original
authored stages, scenery, weapon selection/upgrades, energy/shields, abilities,
saves, and full shop/progression menus are not ported yet; inactive HUD meters remain empty. Web/mobile
builds are also pending. Existing music remains in use.

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

The headless combat slice runs the same fixed-tick simulation as the client,
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
