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
    coarse = noise(4, 9, seed)
    fine = noise(13, 26, seed + 10)
    details = noise(37, 85, seed + 20)
    palettes = {
        "Tide": [(15, 42, 104), (18, 51, 119)],
        "Shoals": [(10, 58, 85), (15, 76, 94), (23, 90, 99), (48, 112, 111)],
        "Vapor": [(36, 40, 82), (42, 47, 92), (52, 54, 100)],
    }
    colors = palettes[style]
    pixels = bytearray(WIDTH * HEIGHT * 4)
    for y in range(HEIGHT):
        phase = 2 * pi * y / HEIGHT
        for x in range(WIDTH):
            i = y * WIDTH + x
            u = x / WIDTH
            n = coarse[i] / 255 - 0.5
            f = fine[i] / 255 - 0.5
            h = details[i] / 255 - 0.5
            if style == "Tide":
                edge = (1-u if near else u)
                field = 0.16 + 0.19*sin(phase*2 + near*2) + 0.09*cos(phase*3) + 0.11*n - edge
                band = near
            elif style == "Shoals":
                edge = (1-u if near else u)
                field = (0.14 if near else 0.27) + 0.16*sin(phase*2 + near*3) + 0.36*n + 0.12*f + 0.04*h - edge
                band = (1 if field > 0.088 else 0) + (2 if field > 0.155 else 0)
            else:
                # Long diagonal streaks, broken into banks by the slower density field.
                ridge = cos(phase*5 + 6.3*u + 1.7*n + 0.58*f)
                field = 0.22*ridge + 0.31*n + 0.10*f + 0.034*h - (0.215 if near else 0.16)
                band = 2 if near else int(field > 0.09)
            if field > 0:
                pixels[i*4:i*4+4] = bytes((*colors[band], min(255, int(field*WIDTH*255))))
    layer = "Near" if near else "Far"
    Image.frombytes("RGBA", (WIDTH, HEIGHT), bytes(pixels)).save(OUT / f"{style}{layer}.png")


if __name__ == "__main__":
    OUT.mkdir(parents=True, exist_ok=True)
    for style in ("Tide", "Shoals", "Vapor"):
        for near in (0, 1):
            bake(style, near)
