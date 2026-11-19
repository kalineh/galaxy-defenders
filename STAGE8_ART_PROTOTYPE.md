# Classic and Modern deck art

This branch adds a reversible presentation experiment, not a gameplay change.
Classic remains the default. Options > Stage art selects Classic or Modern.
Press F9 to switch the same setting at any time during play or in menus.
The console accepts `render.style classic` and `render.style modern`.
| Stage | Deck family | Finish |
| --- | --- | --- |
| 4 | Station panels | Dry, matte metal |
| 7 | Station panels | Dry, matte metal |
| 8 | Naval plating | Brushed metal over a hazy sea |
| 10 | Service channels | Wet sheen in rain |
| 11 | Naval plating | Brushed metal |

## What changes

- The authored deck footprints (including the existing 2 px
  anti-gap apron) are rasterized as a single stage-space union at initial use.
- One continuous brush-grain material spans the union. Only exposed edges get
  directional bevels, including concave corners. Internal authored tile joins
  do not create seams. Chunk borders have no bevels and share world coordinates.
- Modern decks now use one reusable parts sheet, authored in
  `art/platforms/stage8-plating.svg`. Four 256-unit panel patterns share seam
  endpoints. Alternating the two face-color banks keeps both sides of every joint
  the same tone across UV cuts. A stage-coordinate hash selects the pattern.
  The inset rail follows distance to the exposed deck edge. Two mirrored miter
  fittings finish inside and outside turns. Louvers, covers, bolt strips and
  occasional hazard paint get a repeatable position and quarter-turn within
  each bay. Their rotated footprint must clear both the rail and plate seams.
- The deck uses bilinear filtering for fractional camera motion. Each strip
  includes one neighboring row at shared ends so the sampling stays continuous.
- Run `python tools/bake_stage8_plating.py` after editing the sheet (Pillow and
  resvg-py). It verifies all neighboring edges before writing the runtime PNG.
- Stage 8 uses one blurred silhouette mask for its two distant layers. The far
  silhouette is 4.8% of foreground size with 3% camera motion. The middle
  silhouette is 16% of foreground size with 8.5% camera motion.
- Some unoccupied platforms narrow briefly along one or both edges. The cut is
  made before the rim and fitting clearance are calculated. Nearby structures,
  enemy placements, secret entrances and adjoining deck pieces constrain it.
- Short square lugs, ledges, stepped brackets and occasional cable loops extend from exposed rails.
  Some inside corners have a diagonal tie beneath them. Shared hardware has a
  short contact shadow and a rounded lip baked from the shape of the part.
- A wrapping material field controls broad gloss on wet panels. It stays fixed
  in world coordinates across strip joins. Falling snow is a foreground effect.
- Buildings, SecretHint, HUD, player/enemy sprites, shots, effects, simulation,
  and original stage data are unchanged. Foreground dust is suppressed only in
  Modern Stage 8 to keep the new distant water/deck separation quiet.

The supplied AI-generated neutral brush-grain and quiet sea tiles are stored in
`assets/textures/stage8-brushed-metal.png` and `stage8-sea.png`. Deck strips are assembled on first use and released on a stage change or shutdown.

## Play and compare

Build normally on Windows: `./build.sh native` (Git Bash, setup.ps1 first).
Run from the native output directory:

```
./galaxy.exe --command 'game.reset 42;player.join 2;game.stage 8 0.4;render.style modern'
```

Backtick opens the console. Enter `render.style classic` / `render.style modern`
without advancing/resetting the stage to compare the identical position.
Options > Stage art also works and persists in the normal settings file.
An explicit launch command wins over an existing saved style.

**Use the provided isolated-profile launcher for this experimental build.**
The added trailing settings field is accepted by this build (old files default
Classic), but older executables reject settings with the extra field. Do not
share this prototype's settings directory with the shipping executable.

## Reproducible captures

Windows memory build: `./build.sh render`; executable is
`build/headless/galaxy-render.exe`. Optional Linux cloud verification uses
`bash scripts/build_memory_linux.sh` (GCC + Python, no additional packages).

```
mkdir -p artifacts/native-port
./build/headless/galaxy-render --command 'game.reset 42;player.join 2;player.invincible 1 1;player.invincible 2 1;game.stage 8 0.4;player.input 1 0 0 1 0;player.input 2 0 0 1 0;game.step 90;render.style modern' --frames 1 --capture artifacts/native-port/stage8-modern.png
```

Replace modern with classic for the matched baseline. Repeat at progress .25
and .6 for different deck layouts. These bounded commands never load/write
human saves or initialize audio. Invincibility is capture setup only.

## Validation

- Linux memory build uses `-Wall -Wextra -Werror` with the pre-existing compressed
  boss.c misleading-indentation warning disabled. New app.c/art code also passes
  strict warnings without that suppression.
- Stage 8 Classic PNG is byte-identical to the original app.c renderer at the
  matched .4/90-tick co-op frame (same simulation source and seed).
- New headless tests cover default Classic, Options toggle, command/assertion,
  setting persistence, old settings-file compatibility and CLI override flag.
- Existing Linux test suite has one pre-existing platform-specific backup-save
  assertion: backups are created under `_WIN32`, but the test runs unconditionally.
- Windows cross-compilation and human GPU/controller playtesting are reported
  separately with the delivery. Software images are not proof of GPU performance.
