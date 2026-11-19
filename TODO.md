# Next gameplay work

## Stage clear and balance

- [x] **Stage clear.** Give the boss defeat a short beat, fly the surviving
  ships off automatically, then fade the playfield and both HUD rails to black
  together. Show the stage name, Stage Clear, three star outlines, the category
  percentages, and the full team credit award. Fill the earned stars in sequence.
  Confirm reveals the full tally immediately, then advances to the shop. Keep
  the final stage connected to the ending. Award and checkpoint the result
  exactly once, including when the player skips the animation.
- [x] **Stars.** Award one for clearing, two for over 75% combined completion,
  and three for a perfect run. Use the average of the applicable enemy,
  building, and coin percentages for the second star. Compare raw counts for
  the third so a rounded 100% cannot hide a missed target. Show empty stars
  and carry the result through stage selection and saved bests.
- [x] **Co-op.** Give each pilot half of credits earned together, including the
  clear award and skill bonus. Show the full award once. A 10,000-credit clear
  gives each pilot 5,000 in co-op and gives the solo pilot 10,000. Preserve
  individual wallets, account for odd credits, and test joining or leaving mid-stage.
  Start with 1.25× enemy and boss health for two active pilots, applied once
  alongside the difficulty multiplier. Check joins and disconnects during bosses.
- [x] **One AI pilot usable in both the game and the headless runner.** Allow
  either slot to be AI-controlled, including a drop-in AI wingmate and two AI
  pilots for a watchable run. Drive ordinary player inputs. Track reachable
  targets, move below them, and fire. In co-op, give the pilots opposite
  preferred halves and let them cross over when useful. Provide an invincible
  targeting run to probe firing windows and a vulnerable run that dodges.
- [x] **Fast campaign simulations.** Run fixed-seed simulations without
  rendering, audio, or frame pacing. Use real stage progression and purchases
  within the pilots' budgets. Record per-stage stars and category counts,
  escaped targets, deaths, boss kill time, loadouts, and credits earned and
  spent. Compare solo and co-op on Normal, Hard, and Really Hard. Replay an
  interesting stage and build in the native game to see why a wave was missed.
- [ ] **Tuning after the baseline.** Compare a pilot investing in one starter
  weapon with one switching to a premium weapon mid-campaign. If catching up
  costs too much, ease the first few marks while preserving relative weapon
  prices and late-tier investment. Keep permanent ownership, coin drops, and
  the current purchase-only shop. Use the run results and human playtests to
  decide whether particular weapons, waves, or difficulty settings need a
  small adjustment. A perfect run can be build-dependent.

### First playthrough results

The 72 campaigns used seeds 42, 73 and 101, all three difficulties, one or
two pilots, two AI modes, and two build policies. Solo used Kazuki and co-op
added Rabbit. Both policies bought generators through ID 2 and a shield
through ID 1. One kept the starting primary and the other switched to Beam
after Stage 4. They followed the main route, attempted 616 stages and
cleared 581. The README has commands for repeating and watching a run.

| Campaign finishes | Solo aim | Co-op aim | Solo dodge | Co-op dodge |
| --- | ---: | ---: | ---: | ---: |
| Normal | 6/6 | 6/6 | 0/6 | 0/6 |
| Hard | 6/6 | 6/6 | 0/6 | 1/6 |
| Really Hard | 6/6 | 6/6 | 0/6 | 0/6 |

1. **Playtest the Stage 3 boss first.** All six distinct vulnerable Really Hard
   trajectories reached it and died with 15–45% of its HP left. The build policies
   are identical this early. A candidate experiment is six extra recovery ticks
   between final-phase volleys on Really Hard. Compare the same seeds and a
   human run before adopting it.
2. **Keep the opening economy for now.** Every solo aim pilot investing in Front
   Laser reached Mk VIII by Stage 6. Switching after Stage 4 bought Beam Mk III
   or IV solo and Mk III for both co-op pilots. Co-op stayed at Beam Mk III in
   Stage 6. If the pause feels too long, trial a Mk IV increment of 18,500
   instead of 19,300 credits. At the Stage 6 shop both Really Hard pilots had
   18,881–19,185 credits across the three seeds. The proposed price would let
   both buy that mark one shop earlier on each seed. The sampled Normal and
   Hard co-op wallets would still fall short.
3. **Teach the Stage 10 rebound visually.** With the basic targeting AI, 12 of
   18 invincible starter runs timed out there. Teaching the pilot to return
   the ball into the barricade brought those same builds to 18/18 finishes.
   Consider a matching flash on the charged ball and the block it opens.
4. **Check perfect-run routing.** Really Hard co-op earned three stars on
   stages 1, 2, 4, 5 and 9. The tested solo builds earned no perfect Really
   Hard clears. Other routes and loadouts remain to be tested.

### Boss playtest follow-up

Stage 3 now has 4, 8 and 12 extra recovery ticks between volleys across
its three phases. The core also spaces individual shots by one extra tick in
phase two and two in phase three. Across the same three seeds, distinct dodge
runs that reached Stage 3 cleared it 6/6 on Normal (previously 3/6), 1/4 on
Hard (3/4), and 1/6 on Really Hard (0/6). Human playtesting on Hard is still
worthwhile. The two purchase policies are identical at this point.

In Stage 10 a pilot can aim the rebound by bumping the ball off-center.
Each bump refills three amber segments. Each charged block impact breaks one
block and reflects the ball off its face or corner. Wall bounces preserve
charges. Empty balls still rebound, and broken blocks return after 30 seconds.

### Shop budget before Stage 12

The last campaign shopping opportunity is after Stage 11. Compare cumulative
purchasing power (starting wallet plus stage earnings) with the prices below.
Intermediate gear purchases use part of that budget. Bonus routes and replays
can change it. The ending separately sets each joined pilot's wallet to
999,999,999 credits.

| Median budget on the main route | Solo | Co-op per pilot |
| --- | ---: | ---: |
| Normal | 220,403 | 117,293 |
| Hard | 257,900 | 138,560 |
| Really Hard | 317,940 | 171,068 |

Each cell covers three invincible seeds on both the starter and Beam policies.

| Kazuki on Normal | Credits |
| --- | ---: |
| Max Front Laser from the starting Mk II | 71,500 |
| Max Beam from scratch | 282,200 |
| Ace, Fusion Generator, Ultimate Shield, max Front Laser, Vulcan and Missile Volley | 227,200 |
| Same equipment with max Beam in place of Front Laser | 437,900 |
| Every shop item and mark after starting gear | 2,468,100 |

Three solo invincible runs (seeds 42, 73, 101) took the authored secret exits
on Stages 2 and 3, returned to the main stage, and bought equipment normally.
On Normal, Kazuki's Stage 2 detour earned 1,010–1,050 credits. Taking both
earned 2,345–2,385 inside the bonus stages. For scale, Missile Volley costs
800 credits for Mk I or 2,200 total for Mk II. With the Stage 4 Beam-switch
policy, the median total budget at the last shop was 221,720 on the main
route, 222,640 with the first detour, and 223,835 with both.

Buying Beam at the first shop while following the same generator and shield
plan reached Mk VII on Normal and Hard, and Mk VIII on Really Hard on all
three seeds, even on the main route. With both detours, those Normal and Hard
builds still needed about 70,000 and 28,600 credits respectively for Mk VIII.

### Weapon price and damage checks

All 306 solo Normal practice builds cleared their test stage with the
invincible aiming pilot. The sample covers every purchasable weapon on
Stages 2–12 with seed 42 and repeats Stages 4, 8 and 12 with seeds 73 and
101. Each trial starts at that stage's estimated budget. Stage results
include the rest of the kit. Range DPS isolates the named mount, includes
both sidekicks where applicable, and uses shop damage units.

- **Mini Shot** is the first price to revisit. On Stage 8, Mk IV cost
  50,700 and delivered 137 sustained DPS straight ahead. Missile Volley Mk V
  cost 23,100 for 144 DPS at that distance. Median boss times were 19.4 and
  15.5 seconds respectively. Try a lower Mini Shot price before changing
  its firing pattern.
- **Plasma** is the primary to playtest. On Stage 4 its loadout took 46.1
  seconds to kill the boss versus 31.0 for Front Laser. Mk IV overcharge
  needs 825 energy for one shot; the practice kit's Booster holds 600.
  The aiming pilot now uses regular fire when it cannot fund the salvo.
- **Charge Shot Mk V** costs 162,500 over Mk IV. Sustained single-target
  damage with the Fusion Generator increased from 748 to 805 DPS. The mark
  also gains a bounce, so judge that increment on groups as well. Its early
  marks performed well.
- **Blade** depends heavily on spacing. Mk IV delivered 720 DPS beside the
  ship and zero on the distant target. An AI that normally stands 265 units
  back understates the value of a melee build.

## Other release polish

Expand the first-run launch into a short, skippable story intro that leads
directly into stage one.

Polish the ending and give secret-stage entry a distinctive transition.
Revisit short story beats between stages after the clear sequence is settled.
Check a full launch, controller, suspend/resume, and save/continue cycle on
Steam Deck hardware.

The current Noto faces stay in use across the localized UI. Playtest the
equipment terminology and story text with speakers of each language.

# Shop UI

Implemented the widescreen shop from [the revised concept](design/shop-target.png).
Runtime uses the firing simulation, independent pilot rails, preview resource
meters, stock and purchase prices. The HUD lists all six slots. In the shop, select a slot to browse its stock. Each pilot
confirms purchases in a centered dialog. Either pilot can confirm Next stage below the loadout to launch. The three anime portraits also
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
Playtest the shared plate families across the campaign and bonus routes.
Compare the Modern enemy, structure and ship finishes with F9 at gameplay scale.
