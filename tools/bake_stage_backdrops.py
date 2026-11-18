"""Bake the two vertically seamless depth layers for each abstract stage backdrop."""
from math import cos, pi, sin
from pathlib import Path
from random import Random

from PIL import Image

WIDTH, HEIGHT = 640, 1280
OUT = Path(__file__).resolve().parents[1] / "assets/textures/scenery"


def noise(cols, rows, seed):
    rng = Random(seed)
    grid = Image.frombytes("L", (cols, rows), rng.randbytes(cols * rows))
    padded = Image.new("L", (cols * 3, rows * 3))
    for i in range(3):
        for j in range(3):
            padded.paste(grid, (i * cols, j * rows))
    return padded.resize((WIDTH, HEIGHT), Image.Resampling.BICUBIC,
                         box=(cols, rows, cols * 2, rows * 2)).tobytes()


def bake(style, near):
    seed = {"Tide": 30, "Shoals": 57, "Vapor": 92}[style] + near
    coarse = noise(3, 5, seed)
    tint = {"Tide": (25, 39, 65), "Shoals": (27, 53, 62), "Vapor": (40, 41, 65)}[style]
    alpha = bytearray(WIDTH * HEIGHT)
    for y in range(HEIGHT):
        phase = 2 * pi * y / HEIGHT
        swell = 0.17 * sin(phase + near * 2) + 0.06 * cos(2 * phase)
        for x in range(WIDTH):
            i = y * WIDTH + x
            u = x / WIDTH
            edge = 1 - u if near else u
            n = coarse[i] / 255 - 0.5
            if style == "Tide":
                density = (0.30 + swell - edge + 0.06*n) / 0.30
            elif style == "Shoals":
                density = (0.36 + swell - edge + 0.40*n) / 0.23
            else:
                ridge = cos(2*phase + 3.7*u + near*2 + 0.70*n)
                density = (ridge + 0.65*n - 0.14) / 1.05
            fade = max(0, min(1, density))
            alpha[i] = round((130 if near else 100) * fade*fade*(3 - 2*fade))

    layer = "Near" if near else "Far"
    image = Image.new("RGB", (WIDTH, HEIGHT), tint)
    image.putalpha(Image.frombytes("L", (WIDTH, HEIGHT), bytes(alpha)))
    image.save(OUT / f"{style}{layer}.png")


if __name__ == "__main__":
    OUT.mkdir(parents=True, exist_ok=True)
    for style in ("Tide", "Shoals", "Vapor"):
        for near in (0, 1):
            bake(style, near)
