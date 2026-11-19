#!/usr/bin/env bash
# Optional cloud verification only; human Windows builds still use build.sh.
set -euo pipefail
cd "$(dirname "$0")/.."
mkdir -p build/headless build/raylib-memory
python tools/compile_ui_strings.py
ray=third_party/raylib/src
for unit in rcore rshapes rtextures rtext rmodels raudio; do
    gcc -std=c99 -D_POSIX_C_SOURCE=200809L -O1 -w \
        -DPLATFORM_MEMORY -DGRAPHICS_API_OPENGL_SOFTWARE \
        -DSW_FRAMEBUFFER_OUTPUT_BGRA=0 -DSW_MAX_TEXTURES=1024 \
        -I. -I"$ray" -c "$ray/$unit.c" -o "build/raylib-memory/$unit.o"
done
gcc -std=c11 -D_POSIX_C_SOURCE=200809L -O1 -Wall -Wextra -Werror -Wno-misleading-indentation \
    -DGALAXY_MEMORY -Isrc -Ibuild -I"$ray" src/*.c build/raylib-memory/*.o \
    -lm -lpthread -ldl -o build/headless/galaxy-render
# Do not replace another directory or link.
if [[ ! -e build/headless/assets ]]; then ln -s ../../assets build/headless/assets; fi
echo 'Built build/headless/galaxy-render (windowless, muted, bounded captures only).'
