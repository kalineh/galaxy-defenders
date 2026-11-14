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
