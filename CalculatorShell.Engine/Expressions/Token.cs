//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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
