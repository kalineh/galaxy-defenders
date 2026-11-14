@echo off
cd /d "%~dp0"
if not exist "build\native\galaxy.exe" (
    echo Build first using Git Bash: ./build.sh native
    pause
    exit /b 1
)
start "Galaxy Defenders" "build\native\galaxy.exe" --volume 0.01
