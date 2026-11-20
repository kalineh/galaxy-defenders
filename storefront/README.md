# Steam storefront

The [30-second gameplay trailer](steam/Super_Galaxy_Defenders_Gameplay_Trailer_1080p.mp4)
is a 1920×1080, 60 fps MP4 with stereo audio. It features solo and local co-op
footage with “Skyward Assault” and “Overdrive” from the game soundtrack.

Open [the contact sheet](preview.jpg) to review the capsule layouts. The source
painting is [main.png](main.png). The transparent [logo.png](logo.png) combines
the two title-screen textures using the game's placement and rotation.

Upload the files in [steam/](steam/) to the matching Steamworks fields. The
library hero has separate transparent logo art. Use the lower-left logo position
in the Steamworks preview. For the app icon, use the 184-pixel JPEG. The
shortcut icon uses the Eagle chassis from the game's Modern atlas.

To regenerate the exports with Python and Pillow, run:

```sh
python tools/build_storefront.py
./build.sh render
python tools/capture_storefront.py
```

The five screenshots are full 1920×1080 gameplay frames captured by the
windowless renderer. Their stage, loadout and timing are recorded in the capture
script. Only the screenshots go in Steam's gameplay screenshot slots.

Keep one Steam event localization CSV per announcement in
[patchnotes/](patchnotes/). Copy the template for the next update and enter
the title, body, subtitle and summary for each language. The remaster file is an
unpublished draft. Upload the event cover and header separately when preparing
the announcement in Steamworks.

Valve documents the upload fields and dimensions in the
[store](https://partner.steamgames.com/doc/store/assets/standard),
[library](https://partner.steamgames.com/doc/store/assets/libraryassets),
[icon](https://partner.steamgames.com/doc/store/assets/community) and
[event](https://partner.steamgames.com/doc/store/assets/eventassets) guides.
