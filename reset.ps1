#!/usr/bin/env pwsh
# Restore the repo to its starting state (including the messy docs/ corpus).
$ErrorActionPreference = 'Continue'
Set-Location -Path $PSScriptRoot

git checkout -- . 2>$null
git clean -fdx --exclude=.git 2>$null

$tmp = if ($env:TEMP) { $env:TEMP } else { [System.IO.Path]::GetTempPath() }

Write-Host "Reset to starting state. If a /goal is still active, run: /goal clear"
