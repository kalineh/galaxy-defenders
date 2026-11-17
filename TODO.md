# Completed focus attack tuning

Shift now follows the current primary level, including stage pickups. Focus
damage and energy use increased by 50% through reproducible native importer
tuning; the legacy Beam focus level-two 0.75 outlier is corrected to 0.075 before
the multiplier. Laser focus upgrades scale fire rate/emitter count, not per-shot
damage. Normal fire, pilot multipliers and generator regeneration are unchanged.
Pickups reset both modes and retire old continuous beams to prevent stacking.
Verified: every primary/focus level, pickup scaling, empty energy, continuous drain,
Beam progression/replacement, full ./test.sh, importer --check and native build.
The 50% balance increase is a first pass for human playtesting.

# Completed enemy follow-up

- Done: fence pairs enforce reciprocal ownership; removed/reused endpoints cannot
  redirect beams or kill unrelated enemies. Drawn beam width matches its collider.
  Verified projectile kills from either side, slot reuse, neighboring pairs,
  full gameplay tests, native build and software-render capture.
- Done: Cutter's grey balls release at orbit-tangent speed, retain their spin and
  damage immunity, and exit normally. Regression checks cover both directions,
  camera scroll, center-slot reuse and eventual removal; core gameplay checks pass.
- Done: orange Globs drop three coins on destruction or attachment expiry;
  regressions check both paths and prevent duplicate payout.
- Verified together: full ./test.sh and native Windows build. Fence rendering was
  checked with the windowless software renderer. Cutter release and Glob rewards
  are intentional gameplay changes requested by the user, beyond legacy parity.

# Completed camera movement audit

Compared boss anchors/attached parts, movement presets, burst pauses, detached
Swirl/Splitter/Pyramid parts, teleport placement and chaser camera handling with
legacy. Fixed Boss Six's random target center and scrolling world target, Boss
Ten's independently moving blocks/balls missing camera scroll, and Spikes missing
scroll and using unpanned screen bounds. Spike edge cooldown/clamp now matches
the original sequence. Existing intentional Glob scroll cancellation is retained.
Verified: full ./test.sh and native Windows build. New regressions cover the
21-block formation, ball spawn vs subsequent scrolling, target stability and
both panned viewport edges. This was a movement/camera audit, not an exhaustive
frame-by-frame gameplay parity review; interactive playtesting remains human-owned.

# Completed enemy movement corrections

Boss Five side parts deploy once, then patrol from their current world position
instead of the stale off-screen spawn anchor. Camera movement carries patrol
endpoints correctly; outbound/return waits follow the legacy movement sequence.
RotateTurret and TripleShot with no mover remain fixed to scenery during bursts.
Explicit movers retain the legacy IgnoreCamera/physical-velocity behavior.
Verified: movement regressions (deployment, return, residual scrolling, stationary
and moving turrets), full ./test.sh, and native Windows build. Human playtest pending.

# Completed save slots / drop-in controllers

Three campaign slots replace solo/co-op saves. Explicit overwrite confirmation,
validated backup recovery, flushed atomic replacement, per-slot pending writes,
visible write errors/retries and non-destructive old-save migration are implemented.
Checkpoints: new-run/stage start, finalized stage-clear rewards, shop purchases.
Mid-stage quit returns to title without saving partial progress.

Device ownership is independent of P1/P2. Start/Options claims a free slot, then
pauses/resumes or readies in the shop. Select/Share + Start leaves; removal drops
out and last-player removal pauses/cancels a pending launch. Equipment, money and
health are retained; reconnect cannot revive a ship. Both loadouts share one save.
Absent players do not block menus/shop; either player can navigate shared menus.
F2 toggles keyboard P2. Native Windows DS4 USB/BT report parsing follows Solcraft,
using a bounded Raw Input transport; standard raylib gamepads remain available.
No sibling dependency or new third-party package was introduced.

Original soundtrack attribution/MIDI metadata now live under legacy. The active
credit and redundant original runtime music copy were removed; archive preserved.

Verified: full ./test.sh, native Windows build, memory-render probes, populated
slot-screen capture. Tests cover slot isolation, overwrite cancel, checkpoint vs
partial-stage quit, corruption recovery, invalid writes, joining/removal, retained
inactive loadout, dead-player rejoin, shared menu ownership, pilot/launch races,
secret return ownership, USB/BT reports, malformed packets and radial deadzone.
Physical DS4 USB/Bluetooth and other controller hardware require human playtesting;
no desktop automation or human save access was used in automated checks.

Deferred as requested: mouse-follow mode, MGUI integration, options redesign.
No key-binding UI added. Music remains ready for human listening feedback.
