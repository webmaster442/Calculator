//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Engine.Simplification;

namespace CalculatorShell.Engine.LogicExpressions;

internal sealed class LogicExpressionParser
{
    private LogicTokenizer _tokenizer;
    private LogicToken _currentToken;
    private readonly string[] _variables;

    private static readonly LogicTokenSet FirstFactor = new(LogicTokenType.Variable, LogicTokenType.OpenParen);
    private static readonly LogicTokenSet FirstFactorPrefix = FirstFactor + LogicTokenType.Constant;
    private static readonly LogicTokenSet FirstUnaryExp = FirstFactorPrefix + LogicTokenType.Not;
    private static readonly LogicTokenSet FirstMultExp = new(FirstUnaryExp);
    private static readonly LogicTokenSet FirstExpExp = new(FirstUnaryExp);

    private bool Next()
    {
        if (_currentToken.Type == LogicTokenType.Eof)
        {
            throw new EngineException("Parsed past the end of the function");
        }
        _currentToken = _tokenizer.Next();

        return _currentToken.Type != LogicTokenType.Eof;
    }

    private void Eat(LogicTokenType type)
    {
        if (_currentToken.Type != type)
        {
            throw new EngineException("Missing: " + type);
        }
        _ = Next();
    }

    private bool Check(LogicTokenSet tokens)
        => tokens.Contains(_currentToken.Type);

    public LogicExpressionParser()
    {
        _tokenizer = new LogicTokenizer(string.Empty);
        _variables = new string[32];
        for (int i = 0; i < 26; i++)
        {
            _variables[i] = ((char)('a' + i)).ToString();
        }
        for (int i = 26; i < 32; i++)
        {
            _variables[i] = $"{(char)('a' + i - 26)}b";
        }
    }

    public ILogicExpression Parse(int variableCount, IReadOnlyList<int> minterms)
    {
        if (variableCount < 2 || variableCount > 26)
            throw new EngineException("Variable count must be between 2 and 26");

        if (minterms.Count == 0)
            throw new EngineException("No minterms were specified");

        return Parse(QuineMcclusky.GetSimplified(minterms, _variables.Take(variableCount).ToArray()));
    }


    public ILogicExpression Parse(string expression)
    {
        _tokenizer = new LogicTokenizer(expression);
        _currentToken = new LogicToken(string.Empty, LogicTokenType.None);
        if (!Next())
        {
            throw new EngineException("Cannot parse an empty function");
        }
        var exp = ParseOrExpression();
        var leftover = string.Empty;
        while (_currentToken.Type != LogicTokenType.Eof)
        {
            leftover += _currentToken.Value;
            _ = Next();
        }
        if (!string.IsNullOrEmpty(leftover))
        {
            throw new EngineException("Trailing characters: " + leftover);
        }
        return exp;
    }

    private ILogicExpression ParseOrExpression()
    {
        if (Check(FirstMultExp))
        {
            var exp = ParseAndExpression();

            while (Check(new LogicTokenSet(LogicTokenType.Or)))
            {
                var opType = _currentToken.Type;
                Eat(opType);
                if (!Check(FirstMultExp))
                {
                    throw new EngineException("Expected an expression after + or - operator");
                }
                var right = ParseAndExpression();

                exp = opType switch
                {
                    LogicTokenType.Or => new OrLogicExpression(exp, right),
                    _ => throw new EngineException("Expected plus or minus, got: " + opType),
                };
            }

            return exp;
        }
        throw new EngineException("Invalid expression");
    }

    private ILogicExpression ParseAndExpression()
    {
        if (Check(FirstExpExp))
        {
            var exp = ParseExpExpression();

            while (Check(new LogicTokenSet(LogicTokenType.And)))
            {
                var opType = _currentToken.Type;
                Eat(opType);
                if (!Check(FirstExpExp))
                {
                    throw new EngineException("Expected an expression after * or / operator");
                }
                var right = ParseExpExpression();

                exp = opType switch
                {
                    LogicTokenType.And => new AndLogicExpression(exp, right),
                    _ => throw new EngineException("Expected mult or divide, got: " + opType),
                };
            }

            return exp;
        }
        throw new EngineException("Invalid expression");
    }

    private ILogicExpression ParseExpExpression()
    {
        if (Check(FirstUnaryExp))
        {
            return ParseNotExpression();
        }
        throw new EngineException("Invalid expression");
    }

    private ILogicExpression ParseNotExpression()
    {
        var negate = false;
        if (_currentToken.Type == LogicTokenType.Not)
        {
            Eat(LogicTokenType.Not);
            negate = true;
        }

        if (Check(FirstFactorPrefix))
        {
            var exp = ParseConstants();

            if (negate)
            {
                return new NotLogicExpression(exp);
            }
            return exp;
        }
        throw new EngineException("Invalid expression");
    }

    private ILogicExpression ParseConstants()
    {
        ILogicExpression? exp = null;
        if (_currentToken.Type == LogicTokenType.Constant)
        {
            exp = new ConstantLogicExpression(Convert.ToBoolean(_currentToken.Value));
            Eat(LogicTokenType.Constant);
        }

        if (Check(FirstFactor))
        {
            if (exp == null)
            {
                return ParseVariables();
            }
            return new AndLogicExpression(exp, ParseVariables());
        }
        // This should be impossible because bad symbols are caught by Tokenizer,
        //  constants would have been parsed in the if-statement above, and
        //  anything else is treated as a Factor (UndefinedVariableException
        //  will be thrown when you try to evaluate the function).
        if (exp == null)
        {
            throw new EngineException("Invalid Expression");
        }
        return exp;
    }

    private ILogicExpression ParseVariables()
    {
        ILogicExpression? exp = null;
        do
        {
            ILogicExpression? right;
            switch (_currentToken.Type)
            {
                case LogicTokenType.Variable:
                    right = new VariableLogicExpression(_currentToken.Value);
                    Eat(LogicTokenType.Variable);
                    break;

                case LogicTokenType.OpenParen:
                    Eat(LogicTokenType.OpenParen);
                    right = ParseOrExpression();
                    Eat(LogicTokenType.CloseParen);
                    break;

                default:
                    throw new EngineException("Unexpected token in Factor: " + _currentToken.Type);
            }

            exp = exp == null ? right : new AndLogicExpression(exp, right);
        } while (Check(FirstFactor));

        return exp;
    }
}
