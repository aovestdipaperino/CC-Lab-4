namespace DocForge;

/// <summary>
/// The house-style engine.
///
/// LAB 4 — both methods are stubs. Build them up over five steps (see STYLE.md):
///   1. whitespace   2. heading style   3. unordered lists
///   4. code fences  5. line length
///
/// The tests in tests/DocForge.Tests/ (StepN…) are the spec, one per rule. The
/// whole point of the lab is that these rules, once distilled into this
/// deterministic engine, apply identically to every one of the 40 documents in
/// docs/ — no per-file prompting, no drift.
/// </summary>
public static class Forge
{
    /// <summary>Return every STYLE.md violation in <paramref name="content"/>.</summary>
    public static IReadOnlyList<Violation> Check(string content)
    {
        // TODO(lab4): detect violations for rules 1..5.
        return Array.Empty<Violation>();
    }

    /// <summary>
    /// Return <paramref name="content"/> rewritten to satisfy STYLE.md.
    /// Must be idempotent: Fix(Fix(x)) == Fix(x).
    /// </summary>
    public static string Fix(string content)
    {
        // TODO(lab4): rewrite content to satisfy rules 1..5.
        return content;
    }
}
