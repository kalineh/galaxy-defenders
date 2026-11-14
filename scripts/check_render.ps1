[CmdletBinding()]
param()
$ErrorActionPreference = 'Stop'
$repoRoot = Split-Path -Parent $PSScriptRoot
Push-Location $repoRoot
try {
    & 'C:\Program Files\Git\bin\bash.exe' ./build.sh render
    if ($LASTEXITCODE -ne 0) { throw 'Software-render build failed.' }
    $scratch = Join-Path $repoRoot 'artifacts\native-port'
    $marker = Join-Path $scratch '.scratch-owner'
    if (!(Test-Path -LiteralPath $scratch)) {
        New-Item -ItemType Directory -Path $scratch | Out-Null
        Set-Content -LiteralPath $marker -Value 'galaxy-defenders/native-port/v1'
    }
    if (!(Test-Path -LiteralPath $marker) -or
        (Get-Content -LiteralPath $marker -Raw).Trim() -ne 'galaxy-defenders/native-port/v1') {
        throw 'Scratch folder has no matching ownership marker; inspect before using it.'
    }
    # Reuse exactly these files, never a timestamped build or capture directory.
    & .\build\headless\galaxy-render.exe --script scripts/checks/render.txt --frames 1 --capture artifacts/native-port/software.png *> artifacts/native-port/software-render.log
    if ($LASTEXITCODE -ne 0) { throw 'Software render failed; see artifacts/native-port/software-render.log.' }
    $log = Get-Content -LiteralPath artifacts\native-port\software-render.log -Raw
    if ($log -notmatch 'Platform backend: MEMORY' -or $log -match 'AUDIO: Device initialized') {
        throw 'Render check must use the memory backend without an audio device.'
    }
    Add-Type -AssemblyName System.Drawing
    $image = [Drawing.Bitmap]::new((Join-Path $scratch 'software.png'))
    $source = [Drawing.Bitmap]::new((Join-Path $repoRoot 'assets\textures\Ball.png'))
    try {
        if ($image.Width -ne 1920 -or $image.Height -ne 1080) { throw 'Wrong render dimensions.' }
        # These three scene probes catch blank output, vertical inversion, and RGBA/BGRA mistakes.
        $probes = @(
            @{ X = 960; Y = 303; Expected = $source.GetPixel(40, 40) },
            @{ X = 880; Y = 880; Expected = [Drawing.Color]::FromArgb(247, 208, 74) },
            @{ X = 1040; Y = 880; Expected = [Drawing.Color]::FromArgb(195, 150, 255) }
        )
        foreach ($probe in $probes) {
            $actual = $image.GetPixel($probe.X, $probe.Y)
            $expected = $probe.Expected
            if ([Math]::Abs([int]$actual.R - $expected.R) -gt 3 -or
                [Math]::Abs([int]$actual.G - $expected.G) -gt 3 -or
                [Math]::Abs([int]$actual.B - $expected.B) -gt 3) {
                throw "Readback probe failed at $($probe.X),$($probe.Y): $actual expected $expected"
            }
        }
    } finally {
        $image.Dispose()
        $source.Dispose()
    }
    Write-Output 'PASS: software render, original sprite, player colors, orientation; no window or audio.'
} finally {
    Pop-Location
}
