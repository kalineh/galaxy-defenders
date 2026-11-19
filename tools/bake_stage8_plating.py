"""Bake the Stage 8 deck parts. Uses the same Pillow/resvg-py tools as the sprite atlas."""
from io import BytesIO
from pathlib import Path

from PIL import Image
from resvg_py import svg_to_bytes


root = Path(__file__).resolve().parents[1]
source = root / "art/platforms/stage8-plating.svg"
target = root / "assets/textures/stage8-plating.png"
png = svg_to_bytes(svg_path=str(source))
sheet = Image.open(BytesIO(png)).convert("RGBA")
if sheet.size != (1024, 576):
    raise ValueError("Expected two banks of four 256px panels above the hardware strip")

banks = []
for offset in (0, 512):
    tiles = []
    for y in (0, 256):
        for x in (offset, offset+256):
            tile = sheet.crop((x, y, x+256, y+256))
            if tile.getchannel("A").getextrema() != (255, 255):
                raise ValueError(f"Transparent pixel in metal plate at {x}, {y}")
            tiles.append(tile)
    banks.append(tiles)

for bank in (0, 1):
    for a in banks[bank]:
        for b in banks[1-bank]:
            if a.crop((255, 0, 256, 256)).tobytes() != b.crop((0, 0, 1, 256)).tobytes():
                raise ValueError("Horizontal plate edges do not match")
            if a.crop((0, 255, 256, 256)).tobytes() != b.crop((0, 0, 256, 1)).tobytes():
                raise ValueError("Vertical plate edges do not match")

target.write_bytes(png)
print("Baked Stage 8 plating. All horizontal and vertical neighbors match.")
