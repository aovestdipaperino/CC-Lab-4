using Xunit;

namespace DocForge.Tests;

// STEP 2 — heading style: one space after the # run, no closing hashes.
[Trait("step", "2")]
public class Step2HeadingTests
{
    [Fact]
    public void Detects_missing_space_after_hashes()
    {
        Assert.NotEmpty(Style.Rule(2, "# Title\n\n##Section\n\nbody\n"));
    }

    [Fact]
    public void Detects_extra_spaces_after_hashes()
    {
        Assert.NotEmpty(Style.Rule(2, "# Title\n\n##   Section\n\nbody\n"));
    }

    [Fact]
    public void Detects_closing_hashes()
    {
        Assert.NotEmpty(Style.Rule(2, "# Title\n\n## Section ##\n\nbody\n"));
    }

    [Fact]
    public void Accepts_a_clean_heading()
    {
        Assert.Empty(Style.Rule(2, "# Title\n\n## Section\n\nbody\n"));
    }

    [Fact]
    public void Fix_clears_and_is_idempotent()
    {
        Style.FixClearsRule(2, "#Title\n\n##Section ##\n\n###   Deep\n\nbody\n");
    }

    [Fact]
    public void Fix_clears_the_whole_corpus()
    {
        Style.CorpusCleanForRule(2);
    }
}
