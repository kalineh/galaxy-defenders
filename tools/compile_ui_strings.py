"""Compile the UTF-8 menu CSV and just the glyphs used by each UI font."""
import csv
import json
from pathlib import Path
from tempfile import NamedTemporaryFile

root = Path(__file__).resolve().parents[1]
with (root / "localization/ui.csv").open(encoding="utf-8-sig", newline="") as source:
    languages, *rows = csv.reader(source, strict=True)
if languages != "en de es fr it ja ko pl pt-BR ru zh-Hans zh-Hant".split():
    raise ValueError("Language order is part of the saved settings format")
seen = set()
cjk_languages = {"ja", "ko", "zh-Hans", "zh-Hant"}
glyphs = [set(range(32, 127)) | {0xa5, 0xb2} for _ in range(2)]
lines = ["static const char *const ui_strings[][GAME_LANGUAGE_COUNT] = {"]
for row in rows:
    if len(row) != len(languages) or not row[0] or row[0] in seen:
        raise ValueError(f"Invalid or duplicate translation row: {row}")
    seen.add(row[0])
    cells = [value or row[0] for value in row]
    for lang, value in zip(languages, cells):
        if value.count("%d") != row[0].count("%d") or "%" in value.replace("%d", ""):
            raise ValueError(f"Invalid placeholders: {lang}, {value}")
        glyphs[lang in cjk_languages].update(map(ord, value))
    lines.append("    {" + ", ".join(json.dumps(v, ensure_ascii=False) for v in cells) + "},")
lines.append("};")
lines.append("static const bool ui_uses_cjk[] = {" +
             ",".join("true" if lang in cjk_languages else "false" for lang in languages) + "};")
for name, codes in zip(("western_glyphs", "cjk_glyphs"), glyphs):
    lines.append("static int " + name + "[] = {" + ",".join(map(str, sorted(codes))) + "};")
output = root / "build/ui_strings.inc"
data = ("\n".join(lines) + "\n").encode("utf-8")
output.parent.mkdir(exist_ok=True)
if not output.exists() or output.read_bytes() != data:
    with NamedTemporaryFile(dir=output.parent, delete=False) as tmp:
        tmp.write(data)
    Path(tmp.name).replace(output)
