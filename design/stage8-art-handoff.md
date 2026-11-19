# Stage 8 art handoff

The first Modern test proved the renderer can join the existing deck pieces and scroll the background layers. **It did not hit the art target.** It reads as the same flat shapes with a faint brushed texture. The next pass needs visibly designed platform assets: readable panel divisions, inset trim, corner pieces, hatches and selective hardware, not more texture noise.

## Concept references

| Image | What to take from it |
| --- | --- |
| [Material target](stage8-art/stage8-material-target.jpg) | **Primary target for close art.** The preferred hand-inked Stage 8 paint-over: dark brushed metal, deliberate panels with bevels, recessed hatches, and a few bolts. Notice the large designed shapes before the texture. Match the material and linework, **not** the factory-floor feeling or the concept’s layout quirks. |
| [Depth target](stage8-art/stage8-depth-target.jpg) | **Primary target for scene composition.** The authored full-size deck and the same angular platform shapes echoed smaller over the sea. Use the same strict overhead view for all layers. The JPEG sketches the idea; the next implementation should use two distant layers, not copy its apparent spacing or exact scale. |
| [Ocean study](stage8-art/stage8-ocean-study.jpg) | Secondary atmosphere reference: open water, faint waves, breathing room between structures. **Do not copy the rigs, legs, visible sides, or water contact.** Platforms belong at flight altitude, not on the sea. |

These are AI concept paint-overs of in-game captures, not production textures and not images to slice into the game. The brushed-metal and sea PNGs under `assets/textures/` are the first test’s material inputs. They are not the complete visual treatment.

## What Modern should look like

- **100% gameplay layer:** Existing authored platform footprints and positions. Directly overhead, no visible side walls, supports, or camera tilt. The decks should look like sections of a floating enemy megastructure, not warehouse flooring. Let some shape edges read as exposed armored hull; keep open sea visible between groups.
- **25–30% intermediate and ~5% far:** Repeat recognizable platform families or groups as separate low-contrast silhouettes. Some distant groups can be more complex in outline than the close deck, but neither distant layer needs bolts or fine seams. Give both slower scroll and more haze, with the far layer slowest and faintest. Scale is visual, not the scroll multiplier. Both layers are decorative only.
- **Keep the gameplay read:** Preserve the current HUD, hitboxes, enemy timing, shot shapes, and player colors. Gold and lavender are player colors; coral and a dark neutral belong to enemies. Reserve the busiest details and brightest accents for combat. No toy-like 3D, mixed pixel/HD rendering, or industrial wall-to-wall floor.

## Suggested next pass

1. Start with **one Stage 8 inside corner** and the adjacent straight run. Use `DarkBlue1` placements in `src/stage_data.inc`; do not reauthor the stage. `src/stage8_art.h` already builds a connected deck mask and samples grain in stage space. `src/app.c` chooses Modern vs. Classic and draws the scenery.
2. Keep the grain and intentional long seams anchored to stage coordinates, not the individual sprite or the screen. Put edge strips on the **exposed boundary of each connected deck**, with explicit inside/outside corner caps. Place the hatches, grilles, inset plates and occasional bolts as deliberate visual features. Do not bevel every placement or stamp a full decorated texture over each piece. Grain should be the last detail noticed, not the only one.
3. Capture a matched Classic/Modern frame, then check two more scroll positions and a short motion sample. Look for stable seams, no lines at the joins or chunk boundaries, a clean turn at the corner, and a background that reads as distant water rather than another factory floor. Compare at play scale, not only zoomed in. Finish this one corner before converting the rest of the stage.

## Run it

See [the prototype notes](../STAGE8_ART_PROTOTYPE.md) for exact build/capture commands. On Windows use an isolated save/settings profile for this experiment. **Esc → Options → Stage art** switches Classic/Modern; the console also accepts `render.style classic` and `render.style modern`. Classic is the comparison baseline and should remain unchanged.
