#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/.."
./build.sh native
export ZIG_LOCAL_CACHE_DIR="$PWD/build/zig-cache"
export ZIG_GLOBAL_CACHE_DIR="$PWD/external/zig-cache"
core=()
for source in src/*.c; do
    [[ "$source" == src/main.c || "$source" == src/app.c ]] || core+=("$source")
done
./external/zig/zig.exe cc -std=c11 -target x86_64-windows-gnu -mcpu=x86_64 -O0 -g \
    -Wall -Wextra -Werror -Isrc -Ithird_party/raylib/src tests/test_audio.c "${core[@]}" \
    build/raylib/*.o -lopengl32 -lgdi32 -lwinmm -o build/headless/test_audio.exe
./build/headless/test_audio.exe
