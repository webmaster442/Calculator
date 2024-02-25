//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Engine.LogicExpressions;

internal readonly struct LogicTokenSet
{
    private readonly uint _tokens;

    public LogicTokenSet(params LogicTokenType[] tokens)
    {
        _tokens = 0;
        foreach (LogicTokenType token in tokens)
        {
            _tokens |= (uint)token;
        }
    }

    public LogicTokenSet(LogicTokenSet set)
    {
        _tokens = set._tokens;
    }

    private LogicTokenSet(uint tokens)
    {
        _tokens = tokens;
    }

    public static LogicTokenSet operator +(LogicTokenSet t1, LogicTokenType t2)
    {
        return new LogicTokenSet(t1._tokens | (uint)t2);
    }

    public bool Contains(LogicTokenType type)
    {
        return (_tokens & (uint)type) != 0;
    }
}
