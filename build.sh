#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")"

target="${1:-native}"
if [[ $# -gt 1 || ! "$target" =~ ^(native|headless|test)$ ]]; then
    echo 'Usage: ./build.sh [native|headless|test]' >&2
    exit 2
fi
zig=./external/zig/zig.exe
if [[ ! -f "$zig" ]]; then
    echo 'Run .\scripts\setup.ps1 first (pinned Zig 0.13.0).' >&2
    exit 1
fi
export ZIG_LOCAL_CACHE_DIR="$PWD/build/zig-cache"
export ZIG_GLOBAL_CACHE_DIR="$PWD/external/zig-cache"
mkdir -p build/native build/headless
flags=(-std=c11 -target x86_64-windows-gnu -mcpu=x86_64 -O0 -g -Wall -Wextra -Werror -Isrc)
core=(src/game.c src/console.c)

if [[ "$target" == headless ]]; then
    "$zig" cc "${flags[@]}" -DGALAXY_HEADLESS src/main.c "${core[@]}" -o build/headless/galaxy.exe
    echo 'Built build/headless/galaxy.exe'
elif [[ "$target" == test ]]; then
    "$zig" cc "${flags[@]}" tests/test_game.c "${core[@]}" -o build/headless/test_game.exe
    echo 'Built build/headless/test_game.exe'
else
    ray=third_party/raylib/src
    mkdir -p build/raylib
    objects=()
    # Rebuild third-party objects only when source, configuration, or build rules change.
    for unit in rcore rshapes rtextures rtext rmodels raudio rglfw; do
        object="build/raylib/$unit.o"
        if [[ ! -f "$object" ]] || [[ build.sh -nt "$object" ]] ||
           [[ -n "$(find "$ray" -type f -newer "$object" -print -quit)" ]]; then
            echo "Compiling raylib $unit"
            "$zig" cc -std=c99 -target x86_64-windows-gnu -mcpu=x86_64 -O2 -w \
                -DPLATFORM_DESKTOP_GLFW -DGRAPHICS_API_OPENGL_33 \
                -I"$ray" -I"$ray/external/glfw/include" -c "$ray/$unit.c" -o "$object"
        fi
        objects+=("$object")
    done
    "$zig" cc "${flags[@]}" -I"$ray" src/main.c src/app.c "${core[@]}" "${objects[@]}" \
        -lopengl32 -lgdi32 -lwinmm -o build/native/galaxy.exe
    if [[ -d assets ]]; then
        mkdir -p build/native/assets
        cp -ru assets/. build/native/assets/
    fi
    echo 'Built build/native/galaxy.exe'
fi
