#!/usr/bin/env bash
# The verification gate. Builds, then runs each lab STEP in order (selected by
# the xUnit "step" trait), stopping at the first step that fails. Each step
# corresponds to one rule in STYLE.md and is verified both on crafted inputs and
# against the real docs/ corpus.
set -uo pipefail
cd "$(dirname "$0")"

echo "== build (Release) =="
dotnet build -c Release
if [ $? -ne 0 ]; then
  echo "CHECK FAILED (build)"
  exit 1
fi

titles=(
  ""
  "Rule 1 - whitespace (trailing space, final newline, blank runs)"
  "Rule 2 - heading style (space after #, no closing #)"
  "Rule 3 - unordered lists (- marker, one space)"
  "Rule 4 - code fences (opening fence declares a language)"
  "Rule 5 - line length (wrap prose over 100 columns)"
)

for step in 1 2 3 4 5; do
  echo "== STEP $step/5: ${titles[$step]} =="
  dotnet test -c Release --no-build --filter "step=$step"
  if [ $? -ne 0 ]; then
    echo "CHECK FAILED (step $step)"
    exit 1
  fi
  echo "  STEP $step PASSED"
done

echo "CHECK PASSED"
exit 0
