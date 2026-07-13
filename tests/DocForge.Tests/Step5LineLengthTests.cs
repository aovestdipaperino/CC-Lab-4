using Xunit;

namespace DocForge.Tests;

// STEP 5 — line length: prose lines <= 100 chars; wrap at word boundaries.
// Code fences, table rows (containing '|'), and single unbreakable tokens are
// left alone.
[Trait("step", "5")]
public class Step5LineLengthTests
{
    private static string Repeat(string word, int count)
        => string.Join(' ', Enumerable.Repeat(word, count));

    [Fact]
    public void Detects_a_long_prose_line()
    {
        var longLine = Repeat("word", 40); // ~199 chars
        Assert.NotEmpty(Style.Rule(5, "# T\n\n" + longLine + "\n"));
    }

    [Fact]
    public void Ignores_long_lines_inside_a_fence()
    {
        var longLine = Repeat("word", 40);
        Assert.Empty(Style.Rule(5, "# T\n\n```text\n" + longLine + "\n```\n"));
    }

    [Fact]
    public void Ignores_long_table_rows()
    {
        var row = "| " + Repeat("cell", 40) + " |";
        Assert.Empty(Style.Rule(5, "# T\n\n" + row + "\n"));
    }

    [Fact]
    public void Ignores_a_single_unbreakable_token()
    {
        var url = "https://example.com/" + new string('a', 120);
        Assert.Empty(Style.Rule(5, "# T\n\n" + url + "\n"));
    }

    [Fact]
    public void Fix_wraps_to_within_the_limit_and_is_idempotent()
    {
        var messy = "# T\n\n" + Repeat("word", 60) + "\n";
        var fixedContent = Forge.Fix(messy);
        foreach (var line in fixedContent.Split('\n'))
        {
            Assert.True(line.Length <= 100, $"line exceeds 100: {line.Length}");
        }

        Assert.Empty(Style.Rule(5, fixedContent));
        Assert.Equal(fixedContent, Forge.Fix(fixedContent));
    }

    [Fact]
    public void Fix_clears_the_whole_corpus()
    {
        Style.CorpusCleanForRule(5);
    }
}
