# UI replacement: visual exploration and interaction plan

User explicitly requests replacing the legacy GUI/shop appearance, responsive
player panels, mobile layout planning and standardized keyboard/controller input.
This supersedes the earlier UI-fidelity restriction for this replacement.
The first two imagegen boards were too text-heavy and displaced the live game
preview. The revised third concept restores the full-height central playfield
between compact P1/P2 shop panels. This is the current direction; these remain
concepts, not implemented screens. Mockup numbers/proportions and device prompts
are illustrative, not gameplay specifications.

Design target:
- Flat surfaces and geometric icons matching existing sprites; bundled Noto type,
  yellow P1 / lavender P2 accents, explicit focus outlines, restrained borders.
- Shop: keep the large live playfield centered, showing each ship firing its
  selected preview equipment. Keep armor/shield/energy meters visible and live.
  Compact side panels contain six category icon tabs, a short item list, explicit
  current/next level plus pips, a price/action and Ready. Avoid paragraphs and
  comparison tables. Browsing previews equipment without purchasing it.
- Preserve useful nesting: category -> item -> explicit Buy/Upgrade action;
  Back reverses one step and restores focus. Remove left/right level shopping.
  Proposed purchase model: Upgrade buys exactly the next level; Buy & Equip
  replaces a weapon. Existing trade-in credit can be included in the shown price
  rather than exposed as reverse-level navigation. Economy changes are not yet
  implemented; the user's preference is toward simpler purchases/upgrades.
- Co-op: independent focus/balance/purchases/readiness and live preview per player.
  Preserve the center arena in solo too; inactive P2 needs only a quiet join hint.
- HUD: compact player rails on wide screens; status strips at narrow widths.
  Preserve the simulation viewport/aspect ratio; do not stretch or crop gameplay.
- Mobile: retain a visible live preview above a compact shop tray, with resource
  meters visible. Use readable touch targets and safe areas; touch invokes the
  same actions. Do not merely shrink the 1920x1080 canvas or hide the preview.

Input contract for implementation:
- Semantic Navigate / Confirm / Back / Pause / Join / Leave actions, with device
  bindings feeding one path. Directional repeat is deliberate; activation is an
  edge, never a held key leaking into the next screen.
- Keyboard arrows/WASD navigate, Enter/Space confirm, Esc backs out (pauses during
  gameplay). Controller d-pad/stick navigate, south confirms, east backs out,
  Start/Options joins an unclaimed device or pauses its player. Preserve explicit
  leave behavior. Start must not act as generic Confirm/purchase in menus.
- Back closes the deepest selection/modal before leaving a screen; restore prior
  focus on return. Shop Ready is a focusable action using normal Confirm, not a
  mandatory special Tab key. Returning to title explains checkpoint behavior.
- Per-player prompt glyphs reflect keyboard, Xbox-style or DS4 input. On touch,
  display tappable controls. Shared menus need explicit navigation ownership;
  shop panels retain independent owners.

Next implementation slice: consolidate input actions and focus/navigation tests,
then prototype the responsive shop with existing stock/economy/preview logic;
carry the resulting components into pause, options, save slots and player HUDs.
Solcraft has Dear ImGui/rlImGui as a local reference. Library choice and C/C++
boundary remain to be settled; no dependency or runtime UI code changed yet.

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
