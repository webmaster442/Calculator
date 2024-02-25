//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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
