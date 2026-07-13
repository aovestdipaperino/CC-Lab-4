# CC-Lab-4 — `docforge`

Lab 4: **distill a recurring task into a deterministic tool.**

`docs/` has 40 messy markdown documents; `STYLE.md` is the house style they
should follow. Rather than hand-prompting Claude to reformat each file (slow,
and it drifts), you build `docforge` — a small .NET tool whose engine enforces
`STYLE.md` — then run it once over the whole corpus.

## Steps

1. **Enable the Stop hook** so Claude keeps working until the gate is green:
   ```sh
   cp .claude/settings.json.disabled .claude/settings.json          # macOS / Linux
   cp .claude/settings.windows.json.disabled .claude/settings.json  # Windows
   ```
   Run `/hooks` to confirm it registered.

2. **Ask Claude:** "make check.sh pass" (`check.ps1` on Windows). The gate builds
   then runs five steps (one per `STYLE.md` rule), stopping at the first failure,
   so Claude implements the rule engine one rule at a time.

3. **Run `/normalize`** once the engine works: it runs `docforge fix docs` over
   all 40 files in a single deterministic pass, then `docforge check docs` to
   confirm zero violations. Compare that to reformatting each file by hand.

Getting through all five rules is not required — the later ones (fences, line
wrapping) are there so a fast run doesn't finish early.

## The tool

- `dotnet run --project src/DocForge -- check docs` — report violations.
- `dotnet run --project src/DocForge -- fix docs` — rewrite files to conform.

## The gate

`./check.sh` (or `.\check.ps1`) builds, then runs step 1 → step 5. Each step is
its trait-tagged tests, verified on crafted inputs and the real corpus. Prints
`CHECK PASSED` / `CHECK FAILED (step N)`.

## Reset

`./reset.sh` (or `.\reset.ps1`) restores the repo — including the messy corpus —
to its starting state.

## Requirements

- .NET 8 SDK
