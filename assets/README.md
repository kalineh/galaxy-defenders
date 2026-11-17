# Original Runtime Assets

These are unmodified copies from the preserved XNA game:

- `textures/Rookie.png`: `legacy/Content/Textures/Player/Rookie.png`.
- `textures/Ball.png`, `Isosceles.png`, `ShootBall.png`, and `Boss1.png`:
  corresponding files in `legacy/Content/Textures/Enemy/`.
- `textures/MiniShot.png` and `EnemyShot.png`: corresponding files in
  `legacy/Content/Textures/Weapons/`.
- HUD panel frames, backgrounds, icons, armor bar, and portraits: corresponding
  files in `legacy/Content/Textures/UI/`. Layout follows `legacy/Hud/Hud.cs`.
- `DotParticle.png` and `TriangleParticle.png`: original effect textures from
  `legacy/Content/Textures/Effects/`, used with the active legacy particle definitions.
- `audio/*.wav`: corresponding files in `legacy/Content/SE/`.

Original game/code/design credit: Kalin. Original soundtrack attribution is
archived in `legacy/MUSIC_CREDITS.md`. Playback uses the new C synth album.

The build copies these files beside the executable. Runtime never reads `legacy/`
or another repository. Copy the whole `build/native/` folder when sharing a build.
# Additional chassis

The Eagle, Crusher, Interceptor, Phoenix, Lightning, Dragon, Demon, and Ace
textures are copied unchanged from `legacy/Content/Textures/Player/`.

Title, Gunthor portrait, pilot arrow and MenuItem textures come unchanged from
`legacy/Content/Textures/UI/`; Cloud is from `Textures/Background/`. Menu sound
effects come from `Content/SE/`; title and pause tracks (`Konami's_Moon_Base`,
`1-X-0`) come from `Content/Music/`. Per the requested font change, all UI now
uses bundled Noto Sans and the console has Noto Sans Mono as a portable fallback.
See `fonts/README.md` for pinned source and license; no built-in raylib text.

Front Laser uses `Content/Textures/Weapons/Laser.png`, the original
`WeaponShootLaser`/`WeaponHitLaser` sound effects, and `Textures/UI/Energy.png`.

Stage 1 uses the original `Enemy/{Turret,DownTurret,MiniSquare,BigBall,Boss1Chunk}`
PNGs, `Music/Boss 9.wav`, and its referenced `Textures/Decoration` and
`Textures/Static` (including destroyed variants) under `textures/stage/`.

Clear-screen MedalClear/Bronze/Silver/Gold/Platinum PNGs are unchanged copies of
`legacy/Content/Textures/UI/`. The Continue button is a lossless PNG conversion
of `legacy/Content/Textures/UI/Xbox/xboxControllerButtonX.tga`.

Asteroid and Cracks are unchanged `legacy/Content/Textures/Entity/` PNGs;
EnemyDie2/3 are unchanged original `legacy/Content/SE/` WAVs.
