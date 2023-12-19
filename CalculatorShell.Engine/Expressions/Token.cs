namespace CalculatorShell.Engine.Expressions;

internal sealed record class Token
{
    public TokenType Type { get; }
    public string Value { get; }
    public Number? Number { get; }

    public Token(string value, TokenType type, Number? n = null)
    {
        Value = value;
        Type = type;
        Number = n;
    }
}
