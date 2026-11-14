#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")"

target="${1:-native}"
if [[ $# -gt 1 || ! "$target" =~ ^(native|headless|render|test)$ ]]; then
    echo 'Usage: ./build.sh [native|headless|render|test]' >&2
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
core=(src/game.c src/menu.c src/console.c)

if [[ "$target" == headless ]]; then
    "$zig" cc "${flags[@]}" -DGALAXY_HEADLESS src/main.c "${core[@]}" -o build/headless/galaxy.exe
    echo 'Built build/headless/galaxy.exe'
elif [[ "$target" == test ]]; then
    "$zig" cc "${flags[@]}" tests/test_game.c "${core[@]}" -o build/headless/test_game.exe
    echo 'Built build/headless/test_game.exe'
else
    ray=third_party/raylib/src
    object_dir=build/raylib
    output_dir=build/native
    executable=galaxy.exe
    platform_flags=(-DPLATFORM_DESKTOP_GLFW -DGRAPHICS_API_OPENGL_33)
    link_flags=(-lopengl32 -lgdi32 -lwinmm)
    units=(rcore rshapes rtextures rtext rmodels raudio rglfw)
    if [[ "$target" == render ]]; then
        object_dir=build/raylib-memory
        output_dir=build/headless
        executable=galaxy-render.exe
        platform_flags=(-DPLATFORM_MEMORY -DGRAPHICS_API_OPENGL_SOFTWARE -DSW_FRAMEBUFFER_OUTPUT_BGRA=0)
        flags+=(-DGALAXY_MEMORY)
        link_flags=(-lwinmm)
        units=(rcore rshapes rtextures rtext rmodels raudio)
    fi
    mkdir -p "$object_dir"
    objects=()
    # Rebuild third-party objects only when source, configuration, or build rules change.
    for unit in "${units[@]}"; do
        object="$object_dir/$unit.o"
        if [[ ! -f "$object" ]] || [[ build.sh -nt "$object" ]] ||
           [[ -n "$(find "$ray" -type f -newer "$object" -print -quit)" ]]; then
            echo "Compiling raylib $unit"
            "$zig" cc -std=c99 -target x86_64-windows-gnu -mcpu=x86_64 -O2 -w \
                "${platform_flags[@]}" \
                -I. -I"$ray" -I"$ray/external/glfw/include" -c "$ray/$unit.c" -o "$object"
        fi
        objects+=("$object")
    done
    "$zig" cc "${flags[@]}" -I"$ray" src/main.c src/app.c "${core[@]}" "${objects[@]}" \
        "${link_flags[@]}" -o "$output_dir/$executable"
    if [[ -d assets ]]; then
        mkdir -p "$output_dir/assets"
        cp -ru assets/. "$output_dir/assets/"
    fi
    echo "Built $output_dir/$executable"
fi
