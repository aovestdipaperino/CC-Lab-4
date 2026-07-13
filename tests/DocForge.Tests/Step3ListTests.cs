using Xunit;

namespace DocForge.Tests;

// STEP 3 — unordered lists: '-' marker, exactly one space after it.
[Trait("step", "3")]
public class Step3ListTests
{
    [Fact]
    public void Detects_star_marker()
    {
        Assert.NotEmpty(Style.Rule(3, "# T\n\n* one\n* two\n"));
    }

    [Fact]
    public void Detects_plus_marker()
    {
        Assert.NotEmpty(Style.Rule(3, "# T\n\n+ one\n"));
    }

    [Fact]
    public void Detects_extra_space_after_dash()
    {
        Assert.NotEmpty(Style.Rule(3, "# T\n\n-   one\n"));
    }

    [Fact]
    public void Accepts_a_clean_list()
    {
        Assert.Empty(Style.Rule(3, "# T\n\n- one\n- two\n"));
    }

    [Fact]
    public void Fix_clears_and_is_idempotent()
    {
        Style.FixClearsRule(3, "# T\n\n* one\n+ two\n-   three\n");
    }

    [Fact]
    public void Fix_clears_the_whole_corpus()
    {
        Style.CorpusCleanForRule(3);
    }
}
