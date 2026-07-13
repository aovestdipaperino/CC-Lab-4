using DocForge;
using Xunit;

namespace DocForge.Tests;

internal static class Corpus
{
    /// <summary>Walk up from the test binary to the lab root (the folder with STYLE.md).</summary>
    public static string Root()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null && !File.Exists(Path.Combine(dir.FullName, "STYLE.md")))
        {
            dir = dir.Parent;
        }

        return dir?.FullName
            ?? throw new DirectoryNotFoundException("could not locate the lab root (STYLE.md)");
    }

    public static IEnumerable<string> Documents()
        => Directory.GetFiles(Path.Combine(Root(), "docs"), "*.md")
            .OrderBy(f => f, StringComparer.Ordinal)
            .Select(File.ReadAllText);
}

internal static class Style
{
    public static IReadOnlyList<Violation> Rule(int rule, string content)
        => Forge.Check(content).Where(v => v.Rule == rule).ToList();

    /// <summary>Fixing clears the rule and is idempotent.</summary>
    public static void FixClearsRule(int rule, string messy)
    {
        var once = Forge.Fix(messy);
        Assert.Empty(Rule(rule, once));
        Assert.Equal(once, Forge.Fix(once));
    }

    /// <summary>Fixing the real corpus clears the rule in every document.</summary>
    public static void CorpusCleanForRule(int rule)
    {
        foreach (var doc in Corpus.Documents())
        {
            Assert.Empty(Rule(rule, Forge.Fix(doc)));
        }
    }
}
