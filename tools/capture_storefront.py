"""Capture Steam screenshots through the game's 1920x1080 software renderer."""

from pathlib import Path
import subprocess


ROOT = Path(__file__).resolve().parents[1]
OUTPUT = ROOT / "storefront" / "steam"
SCENES = [
    ("01_Opening", "game.stage 1 0.32;debug.ai 1 aim;game.step 105"),
    ("02_Coop", "debug.run 1 2 aim starter 7;game.stage 3 0;game.step 1655"),
    ("03_Decks", "debug.loadout 8;game.stage 8 0.31;debug.ai 1 aim;game.step 145"),
    ("04_Boss", "debug.loadout 5;game.stage 5 1;debug.ai 1 aim;game.step 240"),
    ("05_Bonus", "debug.loadout 7;game.stage 15 0.48;debug.ai 1 aim;game.step 190"),
]


if __name__ == "__main__":
    OUTPUT.mkdir(parents=True, exist_ok=True)
    for name, commands in SCENES:
        subprocess.run([ROOT / "build/headless/galaxy-render.exe", "--mute", "--frames", "1",
                        "--command", "render.style modern;" + commands,
                        "--capture", OUTPUT / f"Screenshot_{name}.png"], cwd=ROOT, check=True)
