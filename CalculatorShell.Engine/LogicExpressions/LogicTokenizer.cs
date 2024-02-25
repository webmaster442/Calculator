//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

namespace CalculatorShell.Engine.LogicExpressions;

internal sealed class LogicTokenizer
{
    private readonly string _expression;
    private int _index;

    public LogicTokenizer(string expression)
    {
        _expression = expression;
        _index = 0;
    }

    public LogicToken Next()
    {
        while (_index < _expression.Length)
        {
            if (IsAllowedToken(_expression[_index]))
            {
                return HandleStrings();
            }
            switch (_expression[_index++])
            {
                case ' ':
                case '\t':
                case '\r':
                case '\n':
                    continue;
                case '!':
                    return new LogicToken("!", LogicTokenType.Not);
                case '&':
                    return new LogicToken("&", LogicTokenType.And);
                case '|':
                    return new LogicToken("|", LogicTokenType.Or);
                case '(':
                    return new LogicToken("(", LogicTokenType.OpenParen);
                case ')':
                    return new LogicToken(")", LogicTokenType.CloseParen);
                default:
                    throw new EngineException($"Invalid token '{_expression[_index - 1]}' in function: {_expression}");
            }
        }

        return new LogicToken(string.Empty, LogicTokenType.Eof);
    }

    private LogicToken HandleStrings()
    {
        StringBuilder temp = new();
        while (_index < _expression.Length &&
            IsAllowedToken(_expression[_index]))
        {
            temp.Append(_expression[_index++]);
        }

        string identifier = temp.ToString();

        if (TryParseConstant(identifier, out bool value))
        {
            return new LogicToken(value.ToString(), LogicTokenType.Constant);
        }

        return new LogicToken(identifier, LogicTokenType.Variable);
    }

    private static bool TryParseConstant(string identifier, out bool value)
    {
        switch (identifier.ToLower())
        {
            case "true":
            case "1":
            case "yes":
                value = true;
                return true;
            case "false":
            case "no":
            case "0":
                value = false;
                return true;
            default:
                value = default;
                return false;
        }
    }

    private static bool IsAllowedToken(char c)
    {
        bool isText = c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
        return isText || c == '1' || c == '0';
    }
}
