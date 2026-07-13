# docforge — distill a task into a deterministic tool

`docs/` holds 40 internal markdown docs. They're inconsistent: trailing
whitespace, wonky headings, mixed list markers, bare code fences, over-long
lines. `STYLE.md` is the house style they should all follow.

You *could* ask Claude to reformat all 40 by hand. That's the trap: it's slow and
it drifts — file 3 and file 37 come out subtly different. Instead, **distill the
rules into a tool**: `docforge`, whose engine is `src/DocForge/Forge.cs`.

- `docforge check docs` — report every STYLE.md violation.
- `docforge fix docs` — rewrite every file to conform (idempotent).

`Forge.Check` and `Forge.Fix` ship as stubs. Build them up over five steps, one
per rule in `STYLE.md`. `./check.sh` (`.\check.ps1` on Windows) is the gate: it
builds, then runs the steps in order and stops at the first that fails.

## Steps
1. **Whitespace** — no trailing spaces, one final newline, no 3+ blank-line runs.
2. **Heading style** — one space after the `#` run, no closing hashes.
3. **Unordered lists** — `-` marker, exactly one space after it.
4. **Code fences** — every opening fence declares a language (bare ` ``` ` → ` ```text `).
5. **Line length** — wrap prose over 100 columns; leave code fences, table rows,
   and unbreakable tokens alone.

## The spec and the gate
The tests in `tests/DocForge.Tests/` (`Step1…` … `Step5…`, tagged
`[Trait("step", "N")]`) are the spec. Each step is checked on crafted inputs
**and** against the real `docs/` corpus (fixing every file must clear that
rule). You are not expected to finish all five — get as far as you can.

## The payoff
Once the engine works, `/normalize` (a shipped slash command) runs
`docforge fix docs` over the whole corpus in one deterministic pass. That is the
lesson: probabilistic instructions become deterministic enforcement.
