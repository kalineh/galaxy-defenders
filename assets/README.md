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
- `audio/The_Voyage.wav`: `legacy/Content/Music/The_Voyage.wav`.
- Other `audio/*.wav`: corresponding files in `legacy/Content/SE/`.

Original game/code/design credit: Kalin. Music credit: RushJet1, as recorded in
`legacy/README.md`. Preserve that attribution. Replacement music is deferred.

The build copies these files beside the executable. Runtime never reads `legacy/`
or another repository. Copy the whole `build/native/` folder when sharing a build.
# Additional chassis

The Eagle, Crusher, Interceptor, Phoenix, Lightning, Dragon, Demon, and Ace
textures are copied unchanged from `legacy/Content/Textures/Player/`.

Title, Gunthor portrait, pilot arrow and MenuItem textures come unchanged from
`legacy/Content/Textures/UI/`; Cloud is from `Textures/Background/`. Menu sound
effects come from `Content/SE/`; title and pause tracks (`Konami's_Moon_Base`,
`1-X-0`) come from `Content/Music/`. The Pericles font used by XNA is not bundled;
native text still uses raylib's default font.
