#!/usr/bin/env pwsh
# The verification gate. Builds, then runs each lab STEP in order (selected by
# the xUnit "step" trait), stopping at the first step that fails.
# PowerShell equivalent of check.sh.
$ErrorActionPreference = 'Continue'
Set-Location -Path $PSScriptRoot

Write-Host "== build (Release) =="
dotnet build -c Release
if ($LASTEXITCODE -ne 0) {
    Write-Host "CHECK FAILED (build)"
    exit 1
}

$titles = @(
    'Rule 1 - whitespace (trailing space, final newline, blank runs)',
    'Rule 2 - heading style (space after #, no closing #)',
    'Rule 3 - unordered lists (- marker, one space)',
    'Rule 4 - code fences (opening fence declares a language)',
    'Rule 5 - line length (wrap prose over 100 columns)'
)

for ($step = 1; $step -le 5; $step++) {
    Write-Host ("== STEP {0}/5: {1} ==" -f $step, $titles[$step - 1])
    dotnet test -c Release --no-build --filter "step=$step"
    if ($LASTEXITCODE -ne 0) {
        Write-Host ("CHECK FAILED (step {0})" -f $step)
        exit 1
    }
    Write-Host ("  STEP {0} PASSED" -f $step)
}

Write-Host "CHECK PASSED"
exit 0
