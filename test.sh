#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")"
./build.sh test
./build.sh headless
./build/headless/test_game.exe
./build/headless/galaxy.exe --script scripts/checks/combat.txt
if ./build/headless/galaxy.exe --script scripts/checks/invalid.txt; then
    echo 'FAIL: invalid script returned success' >&2
    exit 1
fi
if ./build/headless/galaxy.exe --unknown; then
    echo 'FAIL: unknown launch argument returned success' >&2
    exit 1
fi
echo 'PASS: unit checks, combat script, and expected command-line failures'
