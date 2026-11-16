#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/.."
export ZIG_LOCAL_CACHE_DIR="$PWD/build/zig-cache"
export ZIG_GLOBAL_CACHE_DIR="$PWD/external/zig-cache"
mkdir -p build/headless artifacts/native-port
./external/zig/zig.exe cc -std=c11 -target x86_64-windows-gnu -mcpu=x86_64 -O2 -Wall -Wextra -Werror -Isrc \
    tools/music_trace.c src/{game,menu,save,console,stage,scenery,movement,enemy,enemy_extra,boss,shop,equipment,weapon,synth}.c \
    -o build/headless/music-trace.exe
python tools/analyze_music.py "$@"
