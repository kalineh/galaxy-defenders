"""Bake the two vertically seamless depth layers for each abstract stage backdrop."""
from math import atan2, cos, hypot, pi, sin
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


SEEDS = {"Tide": 30, "Shoals": 57, "Vapor": 92, "Strata": 121, "Halo": 144,
         "Cluster": 84, "Storm": 15, "Rift": 42, "Ripple": 51, "Mist": 64, "Eclipse": 79}


def bake(style, near):
    seed = SEEDS[style] + near
    coarse = noise(3, 5, seed)
    grain = Random(seed + 200).randbytes(WIDTH * HEIGHT)
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
            elif style == "Halo":
                arc = cos(phase + 2.4*(u - 0.3)**2 + near*1.8 + 0.22*n)
                density = (arc - 0.35) / 0.7
            elif style == "Strata":
                fold = cos(phase*2 - 3.2*u + near + 0.4*n)
                density = (fold - 0.12 + 0.5*n) / 0.9
            elif style == "Cluster":
                # Scattered distant lights, grouped into irregular pockets of space.
                px, py = u*16, y/HEIGHT*32
                cell = (int(px)*37 + int(py)*19 + seed) % 97
                distance = hypot(px%1 - 0.25 - cell%5*0.10, py%1 - 0.25 - cell%7*0.07)
                density = max(0, 1-distance*18) if cell%4 == 0 and n > -0.09 else 0
            elif style in ("Storm", "Ripple", "Eclipse"):
                # Wrap vertical distance so individual landmarks scroll across the seam.
                dx = u - (0.18 if near else 0.86)
                dy = 2*((y/HEIGHT + near*0.43) % 1 - 0.5)
                radius = hypot(dx, dy)
                if style == "Storm":
                    spiral = cos(2*atan2(dy, dx) + 12*radius + 1.4*n)
                    density = max(0, 1-radius*2) * (0.55 + 0.45*spiral)
                elif style == "Ripple":
                    density = max(0, 1-radius*1.9) * max(0, cos(42*radius + n))**2
                else:
                    density = max(0, 1-abs(radius - (0.56 if near else 0.81))*12)
            elif style == "Rift":
                fork = abs(u - 0.50 - 0.15*sin(phase) - 0.3*n)
                density = max(0, 1-abs(fork - 0.20*(1+cos(phase)))*7)
            elif style == "Mist":
                density = (n + 0.08*cos(phase*2 + u*6) - 0.04)*3.4
            else:
                ridge = cos(2*phase + 3.7*u + near*2 + 0.70*n)
                density = (ridge + 0.65*n - 0.14) / 1.05
            fade = max(0, min(1, density))
            coverage = fade*fade*(3 - 2*fade)
            # Fixed grain travels with the wash and breaks up quantization on its slopes.
            jitter = (grain[i]/255 - 0.5) * 32 * coverage*(1-coverage)
            alpha[i] = round((130 if near else 100)*coverage + jitter)

    layer = "Near" if near else "Far"
    image = Image.new("RGB", (WIDTH, HEIGHT), "white")
    image.putalpha(Image.frombytes("L", (WIDTH, HEIGHT), bytes(alpha)))
    image.save(OUT / f"{style}{layer}.png")


def bake_weather():
    for name in ("Snowflake", "Raindrop"):
        alpha = bytearray(64*64)
        for y in range(64):
            v = (y + 0.5)/64
            for x in range(64):
                u = (x + 0.5)/32 - 1
                if name == "Snowflake":
                    coverage = (1 - hypot(u, 2*v-1)) / 0.45
                else:
                    # The narrow tail fades into a rounder leading bead.
                    coverage = (1 - hypot(u / (0.18 + 0.75*v), 2*v-1)) * 5 * v
                alpha[y*64+x] = round(255 * max(0, min(1, coverage)))
        image = Image.new("RGB", (64, 64), "white")
        image.putalpha(Image.frombytes("L", (64, 64), bytes(alpha)))
        image.save(OUT / f"{name}.png")


if __name__ == "__main__":
    OUT.mkdir(parents=True, exist_ok=True)
    bake_weather()
    for style in SEEDS:
        for near in (0, 1):
            bake(style, near)
