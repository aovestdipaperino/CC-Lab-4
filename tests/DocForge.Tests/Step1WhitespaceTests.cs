using Xunit;

namespace DocForge.Tests;

// STEP 1 — whitespace: no trailing spaces, exactly one final newline, no run of
// three or more blank lines.
[Trait("step", "1")]
public class Step1WhitespaceTests
{
    [Fact]
    public void Detects_trailing_whitespace()
    {
        Assert.NotEmpty(Style.Rule(1, "# Title\n\na line with trailing spaces   \n"));
    }

    [Fact]
    public void Detects_missing_final_newline()
    {
        Assert.NotEmpty(Style.Rule(1, "# Title\n\nbody"));
    }

    [Fact]
    public void Detects_excess_blank_lines()
    {
        Assert.NotEmpty(Style.Rule(1, "# Title\n\n\n\nbody\n"));
    }

    [Fact]
    public void Fix_clears_and_is_idempotent()
    {
        Style.FixClearsRule(1, "# Title  \n\n\n\nbody   ");
    }

    [Fact]
    public void Fix_clears_the_whole_corpus()
    {
        Style.CorpusCleanForRule(1);
    }
}
