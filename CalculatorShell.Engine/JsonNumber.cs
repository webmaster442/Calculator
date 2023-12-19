namespace CalculatorShell.Engine;

public sealed record class JsonNumber
{
    public required string Type { get; init; }
    public required string Value { get; init; }
}
