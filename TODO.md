# Active work

Thread: `01a1db6e-6640-75d3-aed6-1b8301bb3878`.
Request: fix every Not OK finding in CONTENT_AUDIT.md. Work on native; commit and
push verified focused pieces. No desktop automation, visible PowerShell windows
or npm installations. All work through `8ae87a2` is committed/pushed.

## Pending user input

OldStage4 has an original building at (325.51, -16555.24) with TextureName="".
The complete archived definition is imported but rejected explicitly on load.
An asynchronous question asks which original building texture should be used.
Do not invent a replacement. Once supplied, apply the explicit correction through
an importer override (preserve the legacy archive), regenerate, run archived and
full checks, capture the stage, update the audit, commit and push.

## Verified work

All identified active-campaign blockers repaired: player survival/contact,
projectile dispatch and shapes, weapon motion/targeting/sprites/effects, enemy/boss
behavior and deaths, powerups, shop stock/previews/departure, timed clear stats/
awards/medals, save records/seed, secret return/camera, scenery order/growth,
.NET-compatible seeded RNG, original sound variants, and state-change fades.
Standalone asteroids and eleven valid OldStage definitions are playable by console.
The original campaign remains twelve main plus seven bonus stages.

Audit corrections: Boss4 movement Fork is commented out; Beam Cue fields are
unused with firing Sound commented out; SpacePlatform.cs is an empty namespace.
Editor-only shader experiments remain archived (no native editor/viewer). Pilot
abilities have no activation callers and remain dormant; do not invent purchases.

## Verification and limits

Full `./test.sh` passes including all 19 campaign and 11 valid archived stages,
explicit rejection of invalid OldStage4, equipment/stage/effect importer checks,
shop/awards/saves/collisions/bosses/RNG/console checks. Native and memory-render
builds pass. Latest one-line trail correction passed core checks and both builds.
Clear/shop/archive captures were inspected under artifacts/native-port/.

Human playtesting, physical controllers, web/phone builds and exhaustive legacy
frame traces remain unverified. Scratch artifacts are disposable. See
CONTENT_AUDIT.md and CONTENT_PARITY.md for the current, qualified status.
