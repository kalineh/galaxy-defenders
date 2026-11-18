# Super Galaxy Defenders â€” Native Port

A plain C/raylib port of the original XNA two-player arcade shooter.
The original source, content, and credits are preserved in [`legacy/`](legacy/README.md).
Work is on `native`; see [the port plan](PORT_PLAN.md) and [current work](TODO.md).

## Play

Run `./play.sh` in Git Bash after building. Normal play starts at 30% master
volume. Choose Play, a save slot, New Game, difficulty and pilots. All
12 main stages and seven bonus stages use their authored event data and synth
music. Stage Clear opens the shop. Up/down selects one of six loadout slots. Confirm opens its stock, up/down
previews equipment, and Back returns to the selected slot. Each pilot keeps purchased gear across stages and can equip it freely.
All twelve generators are available in each shop. Open a weapon to browse its marks.
Primary DPS estimates direct normal-fire damage with all emitters on target and enough energy to fire. A purchase includes every mark up to the
selection and charges only for marks that pilot still needs. Confirm a paid
purchase in the center dialog. Older saves start with their equipped loadouts. Square (C on keyboard) toggles Ready.
The next stage starts when every joined player is ready.
The last main stage opens the original ending and unlocks stage selection.
Play selects one of three campaign slots in `%LOCALAPPDATA%/GalaxyDefenders/slot1.save`
through `slot3.save`. New Game confirms before replacing a populated/unreadable slot.
Checkpoints save on new-run/stage start, stage-clear rewards and shop purchases.
Pause-menu Quit returns to the title without saving partial-stage progress.
Writes flush to a temporary file before atomic replacement and retain a validated
previous `.bak`; loading can recover that backup. Failed writes remain pending,
retry every five seconds and show an error. Existing `solo.save` / `coop.save`
import into empty slots 1 / 2 without deleting the original files. Headless and
bounded render checks never read or write human saves.

Menus use arrows, X to confirm and Z to cancel or gamepad D-pad/A/B. Player two uses J/L,
right Ctrl and Backspace in pilot selection. Original difficulty/pilot loadouts,
generators and shields are connected; Rabbit starts with Lightning. The Options menu saves volume, screen size,
render scale, and anti-aliasing alongside the save slots. Render Scale uses 75%,
100%, 150%, or 200% of the displayed game dimensions. MSAA defaults to 2x and supports Off, 2x, and 4x. FXAA can be toggled
independently of MSAA. Graphics settings apply live in the Windows client. The windowless
verification renderer uses its fixed reference resolution.

Gameplay and equipment sprites are baked from `art/sprites/` into one flight atlas
and one structures atlas. Run `python tools/bake_sprite_atlas.py` after editing the
masters (offline dependencies: Pillow and resvg-py).

`game.reset` is a debug reset (Rookie, level 2 Front Laser), not pilot new-game
setup. `game.stage 1..12 [0..1]` seeks a main stage; `13..19` selects bonus stages.
Seeking preserves equipment/money, restores armor and synchronizes music.

`debug.shop` jumps to the shop immediately. From a campaign it uses the normal shop checkpoint.
From the title or campaign setup it opens a temporary run.

`debug.shadows` toggles soft, centered shadows around player ships, sidekicks, enemies and buildings. They start enabled
each launch. Use `debug.shadows on` or `debug.shadows off` for matching captures.

`debug.music` opens the developer album (Up/Down selects; Left/Right seeks).
`debug.music 1..19 [0..1]` auditions a track from normalized progress;
`debug.music off` restores normal music. It works in menus and during play.
See [music audition instructions](music/README.md) and the
[stage/music timing analysis](MUSIC_TIMING.md). The C synth album supplies all campaign and menu music.

| Action | Keyboard |
| --- | --- |
| Move | Arrows |
| Fire / confirm | X |
| Focus fire / ready | C |
| Cancel / sidekick | Z |
| Join | F2 or Enter |
| Pause / resume | Enter |
| End-of-stage retry | D |

The keyboard can control either player. Press Start/Options on an unassigned
gamepad to join. During play or in the shop, use left/right to pick a pilot in
your player panel the first time you join. Reassigning an existing pilot resumes that pilot immediately. A returning slot keeps its equipment,
money and health, including a destroyed ship.

Open **Pause > Controllers** to see the keyboard and connected gamepads. Any
device can select a row with up/down. Move it left or right between P1,
Unassigned and P2. Assigning to an occupied player releases their previous device.
Start pauses or resumes after joining. Select/Share + Start/Options drops out.
Disconnected pilots stay in their save slot. Disconnecting the last player opens Controllers and freezes play until a device is assigned.
Both loadouts stay in the same campaign save even when one player is absent.
Continuing restores the currently joined party, with the other loadout held for
later joining. Either player can navigate shared menus; absent players do not
block pilot selection or shop readiness.

Standard gamepads use raylib/GLFW. Windows additionally reads up to two native
DS4 controllers (USB/Bluetooth), with report layouts based on Solcraft; no sibling
repository or emulation driver is required. Move with stick/D-pad, fire with
A/Cross or RT/R2, focus with B/Circle or LB/L1, sidekick with RB/R1. Menus accept stick/D-pad,
A/Cross and B/Circle. Analog movement uses a radial deadzone and normalized diagonal
speed. DS4 report parsing, disconnect/claim behavior and retained loadouts are
headless-tested; physical USB/Bluetooth hardware still needs human playtesting.

The Windows transport uses [Raw Input](https://learn.microsoft.com/en-us/windows/win32/inputdev/using-raw-input)
with a hidden message window, bounded report buffers and removal notifications.
Bounded/headless render checks never initialize that transport. Mouse-follow mode,
MGUI integration and the options redesign are deferred; no rebinding UI was added.

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
F3 (or `perf on` / `perf off`) toggles the frame graph. Gold shows the raw time
between game frames, including waits. Cyan shows GPU time for the scene and
MSAA resolve. Update measures CPU time for game logic, input and audio updates.
The graph keeps 240 frames. The readout shows averages and the longest interval
over the last half second. The guide marks the cap or the display interval.
F2 joins with the keyboard. On Windows, Alt+Enter toggles between windowed
and borderless fullscreen and saves the selection in Options.

In Options, **Frame rate cap** defaults to **Display** and **V-sync** defaults
to **On**. Display follows the monitor containing the window. Choose a fixed
cap from 30 to 360 FPS or Unlimited. V-sync can also limit presentation to
the monitor refresh rate. Both settings apply immediately and are remembered.
Ships, shots, pickups and the scrolling world interpolate between the 60 Hz
simulation ticks. With V-sync enabled, interpolation follows the expected frame
cadence through small variations in driver wake-up time. Gameplay and stage
events use elapsed time. On a fixed-refresh display, caps that do not divide
the refresh rate evenly can hold some frames on screen for an extra refresh.

The content is playable, with remaining fidelity work tracked in CONTENT_PARITY.md:
projectile effects/audio, salvage, menu transitions and
precise boss timing. Windows is verified; web/phone builds remain pending.

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
`player.upgrade 1 7` sets the current primary's level (zero-based 0â€“7, matching the legacy
data). It changes both normal and focus patterns without charging money. Inspect
`p1.weapon_level` and `p1.energy` through assertions or `state`.
`game.stage STAGE [PROGRESS]` seeks stages 1â€“12 or bonuses 13â€“19, with normalized progress from 0 to 1.
It replays nominal stage time (0 = start; 0.9 = boss section), rebuilding enemy
positions, bullets, camera stop and music events. The camera waits at the boss,
so progress 1 does not kill it. Equipment/money survive; armor is restored.
Music seeks to the current track's elapsed time, including the boss-track switch.
All 19 stage streams are implemented. `state` reports equipment and currency;
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


## Localization

F8 cycles the UI language. Shift+F8 cycles backward. Options also offers a
Language row, and normal play remembers the selection. The catalog covers
menus, equipment names, tooltips, controller setup, pilot biographies, stage
names, results and campaign dialogue across English, German,
Spanish, French, Italian, Japanese, Korean, Polish, Brazilian Portuguese,
Russian, Simplified Chinese and Traditional Chinese.

The UI uses the bundled Noto Sans faces for Latin and Cyrillic text and
Noto Sans CJK for Japanese, Korean and both Chinese options.

Edit `localization/ui.csv` in UTF-8. The English cell identifies the phrase.
Empty translations use English. Preserve `%d` in numbered labels. The graphics
build validates the CSV and compiles its strings and font glyph lists.
