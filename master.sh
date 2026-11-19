#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")"

usage() {
    echo 'Usage: ./master.sh [--stage-only|--preview]'
    echo 'Default: build and upload the Windows depot. --preview runs a SteamPipe preview.'
    echo '--stage-only builds build/steam/content locally. See steam.local.sh.example for upload settings.'
}

mode=${1:-upload}
if [[ $# -gt 1 ]]; then usage >&2; exit 2; fi
case "$mode" in
    --help|-h) usage; exit 0 ;;
    upload|--stage-only|--preview) ;;
    *) usage >&2; exit 2 ;;
esac

if [[ -f steam.local.sh ]]; then source ./steam.local.sh; fi
steam_username=${STEAM_USERNAME:-mrcrops}

bash ./build.sh steam
version=$(./build/steam/content/galaxy.exe --version | tr -d '\r')
[[ "$version" == v* ]] || { echo 'Could not read the packaged game version.' >&2; exit 1; }
echo "Windows depot: $version"
if [[ "$mode" == --stage-only ]]; then exit 0; fi

steamcmd=${STEAMCMD:-$(command -v steamcmd.exe || true)}
if [[ -z "$steamcmd" ]]; then
    steamcmd=external/steamcmd/steamcmd.exe
    if [[ ! -f "$steamcmd" ]]; then
        mkdir -p external/steamcmd
        curl --fail --location --retry 2 'https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip' -o build/steam/steamcmd.zip
        python -m zipfile -e build/steam/steamcmd.zip external/steamcmd
    fi
fi
preview=0
if [[ "$mode" == --preview ]]; then preview=1; fi
build_dir=$(cygpath -am build/steam)
mkdir -p build/steam/output
cat > build/steam/app_build.vdf <<EOF
"AppBuild"
{
    "AppID" "5664310"
    "Desc" "Super Galaxy Defenders $version (Windows x64)"
    "ContentRoot" "$build_dir/content"
    "BuildOutput" "$build_dir/output"
    "Preview" "$preview"
    "Depots"
    {
        "5664311"
        {
            "FileMapping" { "LocalPath" "*" "DepotPath" "." "Recursive" "1" }
        }
    }
}
EOF
"$steamcmd" +login "$steam_username" +run_app_build "$build_dir/app_build.vdf" +quit
