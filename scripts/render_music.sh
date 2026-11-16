#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/.."
export ZIG_LOCAL_CACHE_DIR="$PWD/build/zig-cache"
export ZIG_GLOBAL_CACHE_DIR="$PWD/external/zig-cache"
mkdir -p build/headless artifacts/native-port
python tools/compile_music.py --check
./external/zig/zig.exe cc -std=c11 -target x86_64-windows-gnu -mcpu=x86_64 -O2 -Wall -Wextra -Werror -Isrc \
    tools/render_music.c src/synth.c -o build/headless/render-music.exe
./build/headless/render-music.exe "${1:-artifacts/native-port/departure-study.wav}"
