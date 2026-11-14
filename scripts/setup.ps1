# Explicit one-time compiler setup. No NPM or sibling repositories are involved.
[CmdletBinding()]
param()
$ErrorActionPreference = 'Stop'
$ProgressPreference = 'SilentlyContinue'
$repoRoot = Split-Path -Parent $PSScriptRoot
$externalRoot = Join-Path $repoRoot 'external'
$compiler = Join-Path $externalRoot 'zig\zig.exe'
if (Test-Path -LiteralPath $compiler) {
    $version = & $compiler version
    if ($LASTEXITCODE -ne 0 -or $version -ne '0.13.0') { throw 'Expected Zig 0.13.0 in external/zig.' }
    Write-Output 'Zig 0.13.0 is ready.'
    exit 0
}
New-Item -ItemType Directory -Path $externalRoot -Force | Out-Null
$archive = Join-Path $externalRoot 'zig-windows-x86_64-0.13.0.zip'
$expectedHash = 'd859994725ef9402381e557c60bb57497215682e355204d754ee3df75ee3c158'
if (!(Test-Path -LiteralPath $archive)) {
    Invoke-WebRequest -UseBasicParsing -Uri 'https://ziglang.org/download/0.13.0/zig-windows-x86_64-0.13.0.zip' -OutFile $archive
}
if ((Get-FileHash -LiteralPath $archive -Algorithm SHA256).Hash.ToLowerInvariant() -ne $expectedHash) {
    throw "Compiler archive checksum mismatch: $archive"
}
Expand-Archive -LiteralPath $archive -DestinationPath $externalRoot -Force
$source = [IO.Path]::GetFullPath((Join-Path $externalRoot 'zig-windows-x86_64-0.13.0'))
$destination = [IO.Path]::GetFullPath((Join-Path $externalRoot 'zig'))
if (!$source.StartsWith($externalRoot + '\') -or !$destination.StartsWith($externalRoot + '\')) { throw 'Invalid setup path' }
if (Test-Path -LiteralPath $destination) { throw 'Incomplete external/zig exists; inspect it before retrying.' }
Move-Item -LiteralPath $source -Destination $destination
& $compiler version
if ($LASTEXITCODE -ne 0) { throw 'Compiler failed to start.' }
