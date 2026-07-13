namespace DocForge;

/// <summary>A single style-rule violation. Rule is 1..5 (see STYLE.md).</summary>
public readonly record struct Violation(int Rule, int Line, string Message);
