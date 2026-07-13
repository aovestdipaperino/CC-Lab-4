---
description: Normalize every doc to STYLE.md using the deterministic docforge tool (not hand edits)
---

The house style in `STYLE.md` has been distilled into a deterministic tool.
Apply it to the whole corpus instead of editing files by hand:

```bash
dotnet run --project src/DocForge -- fix docs
```

Then confirm nothing is left over:

```bash
dotnet run --project src/DocForge -- check docs
```

Report how many files changed and confirm the check reports zero violations. Do
**not** hand-edit the markdown — the point of this lab is that one deterministic
tool applies the rules identically to all 40 files, where per-file prompting drifts.
