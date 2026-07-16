#!/usr/bin/env bash
# Restore the repo to its starting state (including the messy docs/ corpus).
set -uo pipefail
cd "$(dirname "$0")"
git checkout -- . 2>/dev/null || true
git clean -fdx --exclude=.git 2>/dev/null || true
echo "Reset to starting state. If a /goal is still active, run: /goal clear"
