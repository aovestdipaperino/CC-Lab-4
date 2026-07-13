#!/usr/bin/env bash
# Restore the repo to its starting state (including the messy docs/ corpus).
set -uo pipefail
cd "$(dirname "$0")"
git checkout -- . 2>/dev/null || true
git clean -fdx --exclude=.git 2>/dev/null || true
rm -f .claude/settings.json
rm -f "${TMPDIR:-/tmp}/docforge-verify-stop.count" "${TMPDIR:-/tmp}/docforge-verify-stop.log"
echo "Reset to starting state."
