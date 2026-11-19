# Next gameplay work

Check how much firing time the early waves allow on Normal and Hard. Compare a
small Normal difficulty reduction with starting one weapon tier higher.

Expand the first-run launch into a short, skippable story intro that leads
directly into stage one.

The current Noto faces stay in use across the localized UI. Playtest the
equipment terminology and story text with speakers of each language.

# Shop UI

Implemented the widescreen shop from [the revised concept](design/shop-target.png).
Runtime uses the firing simulation, independent pilot rails, preview resource
meters, stock and purchase prices. The HUD lists all six slots. In the shop, select a slot to browse its stock. Each pilot
confirms purchases in a centered dialog and toggles Ready with Square. The three anime portraits also
appear in pilot selection and the gameplay HUD.

The title, campaign setup, pilot selection, pause/options, results, and flight
HUD now use the shop palette and controls. Shop and flight share the pilot
header and resource meters. Compact mobile layout and touch input remain open.

# Completed small enemy reward adjustment

Spikes now drop one coin and Fence endpoints one coin each (two per destroyed
pair), through the existing death/drop path. Splitter fragments remain at three
coins each. Verified existing gameplay/console tests and native Windows build.

# Completed reward scan

Spikes and Fence endpoints dropped zero coins in legacy; their native rewards
were subsequently adjusted above. Splitters pay through two
fragments (three coins each). Boss parts, beams, black holes and asteroids also
have no loose-coin reward. Released Cutter balls intentionally drop nothing.
Two authored Turrets and two Cannons explicitly suppress their usual coin drops.
Legacy buildings supported coin drops, but all 471 authored buildings have zero
coins. Building destruction still awards score that becomes money on stage clear;
the port preserves both behaviors. No reward data was changed in this scan.

# Completed focus attack tuning

Shift now follows the current primary level. Focus
damage is 25% above the original baseline and energy use is 50% higher through native importer
tuning; the legacy Beam focus level-two 0.75 outlier is corrected to 0.075 before
the multiplier. Laser focus upgrades scale fire rate/emitter count, not per-shot
damage. Normal fire, pilot multipliers and generator regeneration are unchanged.
The dormant upgrade-pickup path also resets both modes and retires old continuous
beams to prevent stacking. No authored stage enables these pickups: all 1,630
enemy/building powerup flags are false. Its tests spawn pickups explicitly; the
pickup changes do not affect normal campaign play. Shop upgrades already scaled
Shift before this tuning pass.
Verified: every primary/focus level, pickup scaling, empty energy, continuous drain,
Beam progression/replacement, full ./test.sh, importer --check and native build.
Revised damage down from +50% following human feedback. Verified all 106 focus
emitters changed only damage; energy/drain and other table fields are unchanged.
Full ./test.sh, importer --check and native rebuild pass with the revised balance.

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

The keyboard and first gamepad share P1 at launch. The second gamepad controls P2.
Pause > Controllers changes assignments for the session. Shared menus accept
every connected device. A pilot stays active until their last device leaves,
and keeps equipment, money and health on rejoin. Both loadouts share one save.
Start/Options pauses or resumes. An unassigned pad can join an open slot with
Start, and an unassigned keyboard can join with F2. Select/Share + Start
releases a pad. Native Windows DS4 USB/BT report parsing follows Solcraft,
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

# Stage 8 art prototype (isolated experiment branch)

Classic/Modern Stage art option implemented; see STAGE8_ART_PROTOTYPE.md.
Review the reusable plate kit and inside-corner trim against the
[Stage 8 art handoff and concept JPEGs](design/stage8-art-handoff.md).
The junction at `game.stage 8 0.4` shows both long rails and recessed fittings.
Before merge: human Windows playtest of scrolling/MSAA/FXAA, art-direction review,
and migration decision for settings compatibility with older executables.
Review the matte, wet and snow finishes on stages 4, 7, 10 and 11 alongside the Stage 8 naval deck.
