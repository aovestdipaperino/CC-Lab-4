#!/usr/bin/env pwsh
# Stop hook (PowerShell): re-run the verification gate whenever Claude tries to stop.
# On success, allow the stop (exit 0). On failure, feed the failing output back to
# Claude on stderr and block the stop (exit 2), so the loop continues.
#
# A counter file caps iterations so a genuinely-stuck loop can't run forever.
$ErrorActionPreference = 'Continue'

$projectDir = $env:CLAUDE_PROJECT_DIR
if (-not $projectDir) {
    $projectDir = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
}

$tmp = if ($env:TEMP) { $env:TEMP } else { [System.IO.Path]::GetTempPath() }
$counter = Join-Path $tmp 'docforge-verify-stop.count'
$log = Join-Path $tmp 'docforge-verify-stop.log'
$max = 15

$count = 0
if (Test-Path $counter) {
    $count = [int](Get-Content $counter -ErrorAction SilentlyContinue)
}
$count++
Set-Content -Path $counter -Value $count

if ($count -gt $max) {
    [Console]::Error.WriteLine("verify-stop: reached max iterations ($max). Not blocking further.")
    Remove-Item $counter -ErrorAction SilentlyContinue
    exit 0
}

& powershell -NoProfile -ExecutionPolicy Bypass -File (Join-Path $projectDir 'check.ps1') *> $log
$status = $LASTEXITCODE

if ($status -eq 0) {
    Remove-Item $counter -ErrorAction SilentlyContinue
    exit 0
}

[Console]::Error.WriteLine("check.ps1 failed (iteration $count/$max). Tail of output:")
Get-Content $log -Tail 30 | ForEach-Object { [Console]::Error.WriteLine($_) }
exit 2
