using Xunit;

namespace DocForge.Tests;

// STEP 4 — code fences: every opening fence declares a language; a bare ``` is
// normalized to ```text.
[Trait("step", "4")]
public class Step4FenceTests
{
    private const string Bare = "# T\n\n```\ncode here\n```\n";
    private const string WithLang = "# T\n\n```text\ncode here\n```\n";

    [Fact]
    public void Detects_bare_opening_fence()
    {
        Assert.NotEmpty(Style.Rule(4, Bare));
    }

    [Fact]
    public void Accepts_fence_with_language()
    {
        Assert.Empty(Style.Rule(4, WithLang));
    }

    [Fact]
    public void Closing_fence_is_not_flagged()
    {
        // The second ``` closes the block; only the opening fence needs a language.
        Assert.Single(Style.Rule(4, Bare));
    }

    [Fact]
    public void Fix_clears_and_is_idempotent()
    {
        Style.FixClearsRule(4, Bare);
    }

    [Fact]
    public void Fix_clears_the_whole_corpus()
    {
        Style.CorpusCleanForRule(4);
    }
}
