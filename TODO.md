# Active Work

## Required scope

Faithful port of the complete original game: all authored main/bonus stages,
enemies, bosses, weapons, loadouts, abilities, shops, upgrades, menus, saves,
progression and effects. The user rejected substitute waves and placeholder
bosses. Preserve legacy behavior and text; never claim data imports are playable.
Use the original music and licensed clean fonts, never raylib's built-in font.
Keep the F3 frame-time graph and scriptable console. No computer-use automation,
no NPM installs, no sibling runtime dependencies. Commit/push verified pieces.

## Latest playtest follow-up

User reported undersized enemy bullets and Stage Clear returning to title.
Bullet visual restored to original 40 pixels, hostile radius consistently 2,
unshielded player radius 16, original circle distance rule and side-gun box
contact restored. Boundary tests pass. Next: original Stage Clear -> shop ->
next authored stage flow; do not hide a title restart behind Continue.

## Current handoff

Thread: `01a1db6e-6640-75d3-aed6-1b8301bb3878`.

- All 19 active stage definitions are imported as 3,448 C events. The strict
  importer accounts for every source field and texture size; `--check` verifies
  generated output. All 14 mover presets have native implementations.
- Generated trial waves are removed. Stage 1 runs authored spawns, movement,
  carrier layout, damageable buildings/destroyed art, original building effects,
  music change and camera stop. Boss1 has original health scaling, paired bursts,
  aimed sweeps, smooth movement and independently destructible side guns.
- `game.stage 1 [0..1]` replays nominal stage time to reconstruct enemies,
  projectiles, camera and music. Equipment/money survive; armor is restored.
  0.9 reaches the boss. Other stage IDs explicitly fail until implemented.
- Title/new-game/difficulty/pilot/pause/options navigation, console help,
  completion/history/editing/scrollback, money/invincibility/chassis commands,
  Front Laser's eight normal/focus levels, Standard Generator and F3 graph work.
- Stage Clear waits for enemies and 220 ticks, then Continue returns to title.
  Campaign results/shop/next-stage behavior remains unfinished.

## Remaining work

- Port pickups/rewards and exact enemy/building scoring, co-op damage, turret
  corpses, collision/player-death behavior, stage-end explosions/flyout/fades,
  randomized cloud initialization, remaining presentation details.
- Complete primary/secondary/sidekick weapons, shield/generator choices, pilot
  loadouts and abilities. Rabbit still uses Front Laser; secondaries are absent.
- Port Stage2?12 and BonusStage1?7 runtime enemies/bosses/scenery/secret handling.
  Their imported event tables are not yet playable.
- Port shop/results/upgrades, saved Continue, complete menu transitions and
  sample ships. Web/phone builds and physical controller testing remain pending.

See CONTENT_PARITY.md for the inventory; legacy source is the specification.

## Verification

Git Bash `./test.sh`, `./build.sh native`; `scripts/check_render.ps1` uses silent
windowless software rendering. New checks cover natural-run/seek reconstruction,
boss cadence, independent side-gun destruction, smooth movement, camera stop,
delayed clear, damageable buildings and strict invalid-seek rejection. Native
quiet audio check confirmed Boss 9 seek=10.933 and reported=10.933 seconds.
Stage 1 carrier/boss captures were inspected. Human playtesting is still needed.
Scratch stays in `artifacts/native-port/`; durable inputs never go there.
