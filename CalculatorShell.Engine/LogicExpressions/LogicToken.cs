namespace CalculatorShell.Engine.LogicExpressions;

internal readonly struct LogicToken
{
    public LogicToken(string value, LogicTokenType type)
    {
        Value = value;
        Type = type;
    }

    public string Value { get; }
    public LogicTokenType Type { get; }
}
