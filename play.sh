#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")"

if [[ ! -f build/native/galaxy.exe ]]; then
    echo 'Build first using Git Bash: ./build.sh native' >&2
    exit 1
fi

exec ./build/native/galaxy.exe --volume 0.01 "$@"
