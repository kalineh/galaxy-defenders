"""Export Steam artwork from the approved squad cover and its fighter painting."""

from pathlib import Path
from PIL import Image, ImageChops, ImageDraw, ImageFont, ImageOps


ROOT = Path(__file__).resolve().parents[1]
STORE = ROOT / "storefront"
OUT = STORE / "steam"
TEXTURES = ROOT / "assets/textures"
LANCZOS = Image.Resampling.LANCZOS


def scaled(image, width):
    return image.resize((width, round(image.height * width / image.width)), LANCZOS)


def title_logo():
    base = Image.open(TEXTURES / "GalaxyDefendersLogo.png").convert("RGBA")
    badge = Image.open(TEXTURES / "SuperWordmark.png").convert("RGBA")
    # Preserve the title screen's size, rotation and offset in the 1920-wide UI.
    unit = base.width / 776
    badge = scaled(badge, round(202 * unit)).rotate(6, Image.Resampling.BICUBIC, expand=True)
    x = round(base.width / 2 - 279 * unit - badge.width / 2)
    y = round(base.height / 2 - 87 * unit - badge.height / 2)
    base.alpha_composite(badge, (x, y))
    return base.crop(base.getchannel("A").getbbox())


def squad_logo(logo, portrait_scale=1.0):
    # Coordinates relative to the 626px wordmark in the approved cover.
    unit = logo.width / 626
    top = round(194 * unit * portrait_scale)
    mark = Image.new("RGBA", (logo.width, top + logo.height))
    for name, x, y, size in [("Gunthor", 318, 44, 198), ("Rabbit", 49, 48, 194),
                             ("Kazuki", 170, 0, 225)]:
        with Image.open(TEXTURES / f"{name}Portrait.png") as src:
            art = src.convert("RGBA").crop((2, 1, src.width - 4, src.height - 5))
        fade = Image.new("L", (1, art.height))
        fade.putdata([round(255 * min(1, (art.height - 1 - i) / (art.height * .20)))
                      for i in range(art.height)])
        art.putalpha(ImageChops.multiply(art.getchannel("A"), fade.resize(art.size)))
        px = (275 + (x - 275) * portrait_scale) * unit
        mark.alpha_composite(scaled(art, round(size * unit * portrait_scale)),
                             (round(px), round(y * unit * portrait_scale)))
    mark.alpha_composite(logo, (0, top))
    return mark.crop(mark.getbbox())


def steam_icon(painting):
    # Frame the illustrated fighter between its nose and exhaust at icon scale.
    icon = painting.crop((615, 10, 1515, 910)).resize((512, 512), LANCZOS)
    closeup = painting.crop((670, 155, 1346, 831))
    sizes = [(16, 16), (32, 32), (48, 48), (64, 64), (128, 128), (256, 256)]
    for name, size in [("Client_Image", 16), ("Client_Icon", 32), ("Community_Icon", 184),
                       ("Shortcut_Icon", 256), ("Icon", 512)]:
        source = closeup if size <= 32 else icon
        source.resize((size, size), LANCZOS).save(OUT / f"{name}_{size}x{size}.png")
    frames = [(closeup if size[0] <= 32 else icon).resize(size, LANCZOS) for size in sizes]
    frames[-1].save(OUT / "Shortcut_Icon.ico", sizes=sizes, append_images=frames[:-1])
    icon.convert("RGB").resize((184, 184), LANCZOS).save(OUT / "Community_Icon_184x184.jpg", quality=95)


# Wide covers use the approved composition directly. Other formats position
# the ship and transparent squad mark separately.
LAYOUTS = [
    ("Header_Capsule", 920, 430, (.5, .40), None),
    ("Small_Capsule", 462, 174, (.5, .49), (.62, .33, .50)),
    ("Main_Capsule", 1232, 706, (.5, .50), None),
    ("Vertical_Capsule", 748, 896, (.72, .50), (.78, .50, .76)),
    ("Library_Capsule", 600, 900, (.71, .50), (.88, .50, .78)),
    ("Library_Header", 920, 430, (.5, .40), None),
    ("Library_Hero", 3840, 1240, (.5, .48), None),
    ("Page_Background", 1438, 810, (.5, .5), None),
    ("Event_Cover", 800, 450, (.5, .5), None),
    ("Event_Header", 1920, 622, (.5, .48), None),
    ("Broadcast_Side_Panel", 155, 337, (.72, .5), (.94, .5, .82)),
]

WIDE_COVERS = {"Main_Capsule", "Header_Capsule", "Library_Header", "Event_Cover"}


def review_sheet():
    sheet = Image.new("RGB", (1200, 800), "#101929")
    draw = ImageDraw.Draw(sheet)
    font = ImageFont.truetype(str(ROOT / "assets/fonts/NotoSans-Regular.ttf"), 16)
    panels = [("Main_Capsule", (24, 25, 680, 366)), ("Header_Capsule", (24, 403, 395, 175)),
              ("Small_Capsule", (24, 620, 290, 110)), ("Library_Capsule", (730, 25, 250, 375)),
              ("Vertical_Capsule", (945, 455, 232, 278)), ("Library_Hero", (434, 455, 485, 157)),
              ("Community_Icon", (1002, 25, 148, 148))]
    for name, (x, y, w, h) in panels:
        path = next(OUT.glob(f"{name}_*x*.png"))
        with Image.open(path) as source:
            tile = ImageOps.contain(source, (w, h), LANCZOS)
        sheet.paste(tile, (x, y))
        draw.text((x, y + tile.height + 5), name.replace("_", " "), font=font, fill="#bbc7d6")
    sheet.save(STORE / "preview.jpg", quality=91)


def main():
    OUT.mkdir(parents=True, exist_ok=True)
    logo = title_logo()
    logo.save(STORE / "logo.png")
    squad = squad_logo(logo)
    compact = squad_logo(logo, .62)
    scaled(squad_logo(logo, .78), 1280).save(OUT / "Library_Logo_1280w.png")
    with Image.open(STORE / "main.png") as source:
        painting = source.convert("RGB")
    with Image.open(ROOT / "design/cover-review/squad-cover.png") as source:
        cover = source.convert("RGB")
    for name, w, h, focus, mark in LAYOUTS:
        frame = ImageOps.fit(cover if name in WIDE_COVERS else painting, (w, h),
                             method=LANCZOS, centering=focus)
        if name == "Small_Capsule":
            # Keep the wordmark legible in Steam's 120x45 search result.
            shade = Image.new("L", (w, 1))
            shade.putdata([round(194 * max(0, min(1, (.78 - x / w) / .34))) for x in range(w)])
            frame.paste((8, 15, 45), (0, 0, w, h), shade.resize((w, h)))
        if mark:
            width, center_x, center_y = mark
            title = scaled(compact if name == "Small_Capsule" else squad, round(width * w))
            frame.paste(title, (round(w * center_x - title.width / 2),
                                round(h * center_y - title.height / 2)), title)
        frame.save(OUT / f"{name}_{w}x{h}.png")
    steam_icon(painting)
    review_sheet()


if __name__ == "__main__":
    main()
