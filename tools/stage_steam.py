"""Stage the Windows executable and its runtime files into the Steam depot."""

from pathlib import Path
import shutil
import tempfile


ROOT = Path(__file__).resolve().parents[1]
STEAM = ROOT / "build" / "steam"


def main():
    content = STEAM / "content"
    if content.resolve() != ROOT / "build" / "steam" / "content":
        raise SystemExit(f"Unexpected Steam staging path: {content.resolve()}")

    with tempfile.TemporaryDirectory(dir=STEAM) as tmp:
        staged = Path(tmp) / "content"
        staged.mkdir()
        shutil.copy2(STEAM / "bin" / "galaxy.exe", staged)
        for pattern in ("audio/BonusGet.wav", "audio/BuildingExplode.wav", "audio/Enemy*.wav",
                        "audio/Menu*.wav", "audio/Player*.wav", "audio/Weapon*.wav",
                        "fonts/*.ttf", "fonts/*.otf", "fonts/*LICENSE*.txt",
                        "shaders/*.fs", "textures/**/*.png"):
            files = sorted((ROOT / "assets").glob(pattern))
            if not files:
                raise SystemExit(f"Missing runtime assets matching {pattern}")
            for source in files:
                if source.stem.endswith("Preview"):
                    continue
                dest = staged / source.relative_to(ROOT)
                dest.parent.mkdir(parents=True, exist_ok=True)
                shutil.copy2(source, dest)
        synth = staged / "assets" / "synth"
        synth.mkdir()
        for track in range(1, 20):
            shutil.copy2(ROOT / "build" / "music" / f"{track}.wav", synth)
        licenses = staged / "licenses"
        licenses.mkdir()
        shutil.copy2(ROOT / "third_party/raylib/LICENSE", licenses / "raylib.txt")
        shutil.copy2(ROOT / "third_party/raylib/src/external/glfw/LICENSE.md", licenses / "glfw.txt")
        if content.exists():
            content.rename(Path(tmp) / "previous")
        try:
            staged.rename(content)
        except OSError:
            previous = Path(tmp) / "previous"
            if previous.exists():
                previous.rename(content)
            raise
    print(f"Staged {content}")


if __name__ == "__main__":
    main()
