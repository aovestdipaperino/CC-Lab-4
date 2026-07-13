# House Markdown Style

Every document under `docs/` must follow these rules. They are intentionally
mechanical — the whole point of this lab is that a *deterministic tool* can
enforce them perfectly across the whole corpus, where per-document prompting
drifts.

The five rules map one-to-one to the five build steps of `docforge`. Each rule
can be both **detected** (`docforge check`) and **fixed** (`docforge fix`).

## Rule 1 — Whitespace
- No trailing whitespace on any line.
- The file ends with exactly one newline character.
- No run of three or more consecutive blank lines (collapse to one).

## Rule 2 — Heading style
- Headings are ATX with exactly one space after the `#` run: `## Section`,
  never `##Section` or `##   Section`.
- No closing hashes: `## Section`, never `## Section ##`.

## Rule 3 — Unordered lists
- Unordered list items use `-` as the marker, never `*` or `+`.
- Exactly one space follows the marker: `- item`, never `-item` or `-   item`.

## Rule 4 — Code fences
- Every opening code fence declares a language. A bare ```` ``` ```` opening
  fence is normalized to ```` ```text ````.

## Rule 5 — Line length
- Outside code fences, no line exceeds 100 characters; long prose lines are
  wrapped at word boundaries.
- Lines with no place to break (a single long token such as a URL) and table
  rows (lines containing `|`) are left alone.
