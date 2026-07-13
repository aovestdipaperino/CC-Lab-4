using DocForge;

if (args.Length < 2 || (args[0] != "check" && args[0] != "fix"))
{
    Console.Error.WriteLine("usage: docforge <check|fix> <dir>");
    return 2;
}

var mode = args[0];
var dir = args[1];
if (!Directory.Exists(dir))
{
    Console.Error.WriteLine($"docforge: no such directory: {dir}");
    return 2;
}

var files = Directory.GetFiles(dir, "*.md", SearchOption.AllDirectories)
    .OrderBy(f => f, StringComparer.Ordinal)
    .ToList();

if (mode == "check")
{
    var total = 0;
    foreach (var file in files)
    {
        foreach (var v in Forge.Check(File.ReadAllText(file)))
        {
            Console.WriteLine($"{file}:{v.Line}: [rule {v.Rule}] {v.Message}");
            total++;
        }
    }

    Console.WriteLine($"docforge: {total} violation(s) across {files.Count} file(s)");
    return total == 0 ? 0 : 1;
}

var changed = 0;
foreach (var file in files)
{
    var content = File.ReadAllText(file);
    var fixedContent = Forge.Fix(content);
    if (!string.Equals(fixedContent, content, StringComparison.Ordinal))
    {
        File.WriteAllText(file, fixedContent);
        changed++;
    }
}

Console.WriteLine($"docforge: fixed {changed} of {files.Count} file(s)");
return 0;
