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
