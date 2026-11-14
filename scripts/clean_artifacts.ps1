# This deliberately accepts no directory argument: its only target is this task's scratch folder.
[CmdletBinding(SupportsShouldProcess = $true)]
param([switch]$Apply)

$ErrorActionPreference = 'Stop'
$repoRoot = [IO.Path]::GetFullPath((Split-Path -Parent $PSScriptRoot))
$scratchRoot = [IO.Path]::GetFullPath((Join-Path $repoRoot 'artifacts\native-port'))
$marker = Join-Path $scratchRoot '.scratch-owner'
$owner = 'galaxy-defenders/native-port/v1'

function Assert-OrdinaryPath([string]$Path) {
    $item = Get-Item -LiteralPath $Path -Force
    if ($item.Attributes -band [IO.FileAttributes]::ReparsePoint) {
        throw "Refusing link/junction/reparse point: $Path"
    }
}

# Validate every ancestor, including ancestors above the checkout, before traversal.
$ancestor = $scratchRoot
while ($ancestor) {
    if (Test-Path -LiteralPath $ancestor) { Assert-OrdinaryPath $ancestor }
    $parent = Split-Path -Parent $ancestor
    if ($parent -eq $ancestor) { break }
    $ancestor = $parent
}
if (!(Test-Path -LiteralPath $scratchRoot)) {
    Write-Output 'No task artifacts exist.'
    exit 0
}
if (!(Test-Path -LiteralPath $marker -PathType Leaf)) {
    throw 'Missing ownership marker; refusing cleanup.'
}
Assert-OrdinaryPath $marker
if ((Get-Content -LiteralPath $marker -Raw).Trim() -ne $owner) {
    throw 'Wrong ownership marker; refusing cleanup.'
}

# Enumerate one level at a time: never recursively enter an unchecked directory.
$pending = [Collections.Generic.Queue[string]]::new()
$entries = [Collections.Generic.List[IO.FileSystemInfo]]::new()
$pending.Enqueue($scratchRoot)
while ($pending.Count) {
    $directory = $pending.Dequeue()
    Assert-OrdinaryPath $directory
    foreach ($entry in Get-ChildItem -LiteralPath $directory -Force) {
        $full = [IO.Path]::GetFullPath($entry.FullName)
        if (!$full.StartsWith($scratchRoot + '\', [StringComparison]::OrdinalIgnoreCase)) {
            throw "Path escapes task scratch directory: $full"
        }
        Assert-OrdinaryPath $full
        if ($full -eq $marker) { continue }
        $entries.Add($entry)
        if ($entry.PSIsContainer) { $pending.Enqueue($full) }
    }
}
$bytes = ($entries | Where-Object { !$_.PSIsContainer } | Measure-Object Length -Sum).Sum
Write-Output ("Task scratch: {0}; {1} entries; {2:N2} MiB" -f $scratchRoot, $entries.Count, ($bytes / 1MB))
$entries | Select-Object FullName, Length
if (!$Apply) {
    Write-Output 'Preview only. Use -Apply to remove these disposable files; -Apply -WhatIf also previews.'
    exit 0
}
if ($PSCmdlet.ShouldProcess($scratchRoot, 'Delete owned scratch contents, preserving ownership marker')) {
    # Deepest first, no recursive deletion, and recheck ancestors before each removal.
    foreach ($entry in ($entries | Sort-Object { $_.FullName.Length } -Descending)) {
        $check = $entry.FullName
        while ($check) {
            Assert-OrdinaryPath $check
            $parent = Split-Path -Parent $check
            if ($parent -eq $check) { break }
            $check = $parent
        }
        if ($entry.PSIsContainer -and (Get-ChildItem -LiteralPath $entry.FullName -Force | Select-Object -First 1)) {
            throw "Directory changed during cleanup; refusing removal: $($entry.FullName)"
        }
        Remove-Item -LiteralPath $entry.FullName -Force -Confirm:$false
    }
}
