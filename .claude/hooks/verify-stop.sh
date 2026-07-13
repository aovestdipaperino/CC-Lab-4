#!/usr/bin/env bash
# Stop hook: re-run the verification gate whenever Claude tries to stop.
# On success, allow the stop (exit 0). On failure, feed the failing output
# back to Claude on stderr and block the stop (exit 2), so the loop continues.
#
# A counter file caps iterations so a genuinely-stuck loop can't run forever.
set -uo pipefail

PROJECT_DIR="${CLAUDE_PROJECT_DIR:-$(cd "$(dirname "$0")/../.." && pwd)}"
COUNTER="${TMPDIR:-/tmp}/docforge-verify-stop.count"
LOG="${TMPDIR:-/tmp}/docforge-verify-stop.log"
MAX=15

count=0
if [ -f "$COUNTER" ]; then
  count=$(cat "$COUNTER" 2>/dev/null || echo 0)
fi
count=$((count + 1))
echo "$count" > "$COUNTER"

if [ "$count" -gt "$MAX" ]; then
  echo "verify-stop: reached max iterations ($MAX). Not blocking further." >&2
  rm -f "$COUNTER"
  exit 0
fi

bash "$PROJECT_DIR/check.sh" > "$LOG" 2>&1
status=$?

if [ $status -eq 0 ]; then
  rm -f "$COUNTER"
  exit 0
fi

echo "check.sh failed (iteration $count/$MAX). Tail of output:" >&2
tail -n 30 "$LOG" >&2
exit 2
