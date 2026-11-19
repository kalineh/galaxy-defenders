"""Export Steam artwork from the cover painting and the two title-screen marks."""

from pathlib import Path
import re

from PIL import Image, ImageDraw, ImageFont, ImageOps


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


def steam_icon():
    regions = (ROOT / "src/sprite_regions.inc").read_text()
    full_size = regions.split("#else")[-1]
    match = re.search(r'"textures/Eagle.png", 0, \{(\d+),(\d+),(\d+),(\d+)\}', full_size)
    x, y, w, h = map(int, match.groups())
    with Image.open(TEXTURES / "atlas/Flight0Modern.png") as atlas:
        hull = atlas.crop((x, y, x + w, y + h)).convert("RGBA")
    gold = ImageOps.colorize(hull.convert("L"), "#201f25", "#ffd452").convert("RGBA")
    gold.putalpha(hull.getchannel("A"))
    gold = scaled(gold.rotate(90, expand=True), 435)
    icon = Image.new("RGBA", (512, 512), (15, 25, 45, 255))
    icon.alpha_composite(gold, ((512 - gold.width) // 2, (512 - gold.height) // 2))
    for name, size in [("Client_Image", 16), ("Client_Icon", 32), ("Community_Icon", 184),
                       ("Shortcut_Icon", 256), ("Icon", 512)]:
        icon.resize((size, size), LANCZOS).save(OUT / f"{name}_{size}x{size}.png")
    icon.save(OUT / "Shortcut_Icon.ico", sizes=[(s, s) for s in (16, 32, 48, 64, 128, 256)])
    icon.convert("RGB").resize((184, 184), LANCZOS).save(OUT / "Community_Icon_184x184.jpg", quality=95)


# Crop focus is expressed in the space available to pan. Logo placement is
# (fractional width, horizontal center, vertical center) in the finished asset.
LAYOUTS = [
    ("Header_Capsule", 920, 430, (.5, .49), (.46, .249, .42)),
    ("Small_Capsule", 462, 174, (.5, .49), (.70, .37, .53)),
    ("Main_Capsule", 1232, 706, (.5, .50), (.46, .249, .39)),
    ("Vertical_Capsule", 748, 896, (.72, .50), (.93, .50, .83)),
    ("Library_Capsule", 600, 900, (.71, .50), (.94, .50, .83)),
    ("Library_Header", 920, 430, (.5, .49), (.46, .249, .42)),
    ("Library_Hero", 3840, 1240, (.5, .48), None),
    ("Page_Background", 1438, 810, (.5, .5), None),
    ("Event_Cover", 800, 450, (.5, .5), (.46, .249, .39)),
    ("Event_Header", 1920, 622, (.5, .48), None),
    ("Broadcast_Side_Panel", 155, 337, (.72, .5), (.94, .5, .82)),
]


def review_sheet():
    sheet = Image.new("RGB", (1200, 800), "#101929")
    draw = ImageDraw.Draw(sheet)
    font = ImageFont.truetype(str(ROOT / "assets/fonts/NotoSans-Regular.ttf"), 16)
    panels = [("Main_Capsule", (24, 25, 680, 366)), ("Header_Capsule", (24, 403, 395, 175)),
              ("Small_Capsule", (24, 620, 290, 110)), ("Library_Capsule", (730, 25, 250, 375)),
              ("Vertical_Capsule", (945, 455, 232, 278)), ("Library_Hero", (434, 455, 485, 157))]
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
    scaled(logo, 1280).save(OUT / "Library_Logo_1280w.png")
    with Image.open(STORE / "main.png") as source:
        painting = source.convert("RGB")
    for name, w, h, focus, mark in LAYOUTS:
        frame = ImageOps.fit(painting, (w, h), method=LANCZOS, centering=focus)
        if name == "Small_Capsule":
            # Keep the wordmark legible in Steam's 120x45 search result.
            shade = Image.new("L", (w, 1))
            shade.putdata([round(194 * max(0, min(1, (.78 - x / w) / .34))) for x in range(w)])
            frame.paste((8, 15, 45), (0, 0, w, h), shade.resize((w, h)))
        if mark:
            width, center_x, center_y = mark
            title = scaled(logo, round(width * w))
            frame.paste(title, (round(w * center_x - title.width / 2),
                                round(h * center_y - title.height / 2)), title)
        frame.save(OUT / f"{name}_{w}x{h}.png")
    steam_icon()
    review_sheet()


if __name__ == "__main__":
    main()
