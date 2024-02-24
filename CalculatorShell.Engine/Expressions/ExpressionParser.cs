using System.Globalization;

namespace CalculatorShell.Engine.Expressions;

internal class ExpressionParser
{
    private Token[] _tokens;
    private Token _currentToken;
    private TokenSet _firstAddExp;
    private IVariables _variables;
    private int _tokenIndex = 0;

    private static readonly TokenSet FirstFunction = new(TokenType.Function);
    private static readonly TokenSet Comparison = new(TokenType.SmallerThan | TokenType.SmallerThanEqual | TokenType.GreaterThan | TokenType.GreaterThanEqual | TokenType.Equal | TokenType.NotEqual);
    private static readonly TokenSet Tennary = new(TokenType.TennaryCheck | TokenType.TennaryElse);
    private static readonly TokenSet FirstFactor = FirstFunction + new TokenSet(TokenType.Variable | TokenType.OpenParen);
    private static readonly TokenSet FirstFactorPrefix = FirstFactor + TokenType.Constant;
    private static readonly TokenSet FirstUnaryExp = FirstFactorPrefix + TokenType.Minus;
    private static readonly TokenSet FirstMultExp = new(FirstUnaryExp);
    private static readonly TokenSet FirstExpExp = new(FirstUnaryExp);
    private readonly IFunctionProvider _functionProvider;

    private bool Next()
    {
        if (_currentToken.Type == TokenType.Eof)
        {
            throw new EngineException("Parsed past the end of the function");
        }

        _currentToken = _tokens[_tokenIndex++];

        return _currentToken.Type != TokenType.Eof;
    }

    private void Eat(TokenType type)
    {
        if (_currentToken.Type != type)
        {
            throw new EngineException("Missing: " + type);
        }
        Next();
    }

    private bool Check(TokenSet tokens)
        => tokens.Contains(_currentToken.Type);

    public ExpressionParser(IFunctionProvider functionProvider)
    {
        _tokens = Array.Empty<Token>();
        _currentToken = new Token("", TokenType.None);
        _firstAddExp = new TokenSet(FirstUnaryExp);
        _variables = null!;
        _tokenIndex = 0;
        _functionProvider = functionProvider;
    }

    public IExpression Parse(string function, CultureInfo culture, IVariables variables)
    {
        _firstAddExp = new TokenSet(FirstUnaryExp);
        _tokens = new Tokenizer(function,
                                culture,
                                _functionProvider.FunctionNames)
                       .Tokenize()
                       .ToArray();

        _currentToken = new Token("", TokenType.None);
        _variables = variables;
        _tokenIndex = 0;

        if (!Next())
        {
            throw new EngineException("Cannot parse an empty function");
        }

        var exp = ParseAddExpression();
        var leftover = string.Empty;

        while (_currentToken.Type != TokenType.Eof)
        {
            leftover += _currentToken.Value;
            Next();
        }

        return !string.IsNullOrEmpty(leftover)
            ? throw new EngineException($"Trailing characters: {leftover}")
            : exp;
    }

    private IExpression ParseAddExpression()
    {
        if (Check(FirstMultExp))
        {
            var exp = ParseMultExpression();

            while (Check(new TokenSet(TokenType.Plus | TokenType.Minus | TokenType.TennaryCheck)))
            {
                if (Check(new TokenSet(TokenType.TennaryCheck)))
                {
                    return ParseTennary(exp);
                }
                var opType = _currentToken.Type;
                Eat(opType);

                if (!Check(FirstMultExp))
                {
                    throw new EngineException("Expected an expression after + or - operator");
                }
                var right = ParseMultExpression();

                exp = opType switch
                {
                    TokenType.Plus => new AddExpression(exp, right),
                    TokenType.Minus => new SubExpression(exp, right),
                    _ => throw new EngineException($"Expected plus or minus, got: {opType}"),
                };
            }

            return exp;
        }
        throw new EngineException("Invalid expression");
    }

    private IExpression ParseMultExpression()
    {
        if (Check(FirstExpExp))
        {
            var exp = ParseHighestLevel();

            while (Check(new TokenSet(TokenType.Multiply | TokenType.Divide | TokenType.Mod)))
            {
                var opType = _currentToken.Type;
                Eat(opType);
                if (!Check(FirstExpExp))
                {
                    throw new EngineException("Expected an expression after * or / operator");
                }
                var right = ParseHighestLevel();

                exp = opType switch
                {
                    TokenType.Multiply => new MultExpression(exp, right),
                    TokenType.Divide => new DivExpression(exp, right),
                    TokenType.Mod => new ModExpression(exp, right),
                    _ => throw new EngineException($"Expected multiply, divide or mod. Got: {opType}"),
                };
            }

            return exp;
        }
        throw new EngineException("Invalid expression");
    }

    private IExpression ParseHighestLevel()
    {
        if (Check(FirstUnaryExp))
        {
            var exp = ParseUnaryExpression();

            if (Check(new TokenSet(TokenType.Exponent) + Comparison))
            {
                var opType = _currentToken.Type;
                Eat(opType);
                if (!Check(FirstUnaryExp))
                {
                    throw new EngineException("Expected an expression after exponent or comparision operator");
                }

                var right = ParseUnaryExpression();

                exp = opType switch
                {
                    TokenType.Exponent => new ExpExpression(exp, right),
                    TokenType.GreaterThan => new GreaterThanExpression(exp, right),
                    TokenType.GreaterThanEqual => new GreaterThanOrEqualExpression(exp, right),
                    TokenType.SmallerThan => new LessThanExpression(exp, right),
                    TokenType.SmallerThanEqual => new LessThanOrEqualExpression(exp, right),
                    TokenType.Equal => new EqualExpression(exp, right),
                    TokenType.NotEqual => new NotEqualExpression(exp, right),
                    _ => throw new EngineException($"Expected exponent or comparison, got: {opType}"),
                };
            }

            return exp;
        }
        throw new EngineException("Invalid expression");
    }

    private TennaryExpression ParseTennary(IExpression check)
    {
        var opType = _currentToken.Type;

        if (Check(new TokenSet(TokenType.TennaryCheck)))
            Eat(opType);
        else
            throw new EngineException($"Expected ?, got: {opType}");

        IExpression trueExpression = ParseUnaryExpression();

        opType = _currentToken.Type;

        if (Check(new TokenSet(TokenType.TennaryElse)))
            Eat(opType);
        else
            throw new EngineException($"Expected :, got: {opType}");

        IExpression falseExpression = ParseUnaryExpression();

        return new TennaryExpression(check, trueExpression, falseExpression);
    }

    private IExpression ParseUnaryExpression()
    {
        var negate = false;
        if (_currentToken.Type == TokenType.Minus)
        {
            Eat(TokenType.Minus);
            negate = true;
        }

        if (Check(FirstFactorPrefix))
        {
            var exp = ParseFactorPrefix();

            if (negate)
            {
                return new NegateExpression(exp);
            }
            return exp;
        }
        throw new EngineException("Invalid expression");
    }

    private IExpression ParseFactorPrefix()
    {
        IExpression? exp = null;
        if (_currentToken.Type == TokenType.Constant)
        {
            exp = new ConstantExpression(_currentToken.Number!);
            Eat(TokenType.Constant);
        }

        if (Check(FirstFactor))
        {
            if (exp == null)
            {
                return ParseFactor();
            }
            return new MultExpression(exp, ParseFactor());
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

    private IExpression ParseFactor()
    {
        IExpression? exp = null;
        do
        {
            IExpression? right = null;
            switch (_currentToken.Type)
            {
                case TokenType.Variable:
                    right = new VariableExpression(_variables, _currentToken.Value);
                    Eat(TokenType.Variable);
                    break;

                case TokenType.Function:
                    right = ParseFunction();
                    break;

                case TokenType.OpenParen:
                    Eat(TokenType.OpenParen);
                    right = ParseAddExpression();
                    Eat(TokenType.CloseParen);
                    break;

                default:
                    throw new EngineException($"Unexpected token in Factor: {_currentToken.Type}");
            }

            exp = (exp == null) ? right : new MultExpression(exp, right);
        }
        while (Check(FirstFactor));

        return exp;
    }

    private IExpression ParseFunction()
    {
        Queue<IExpression> parameters = new();
        string name = _currentToken.Value;
        var opType = _currentToken.Type;

        int argumentCount = _functionProvider.ArgumentCount(name);
        if (argumentCount < 1)
            throw new EngineException($"Unexpected Function type: {name}");

        Eat(opType);
        Eat(TokenType.OpenParen);
        for (int i = 0; i < argumentCount; i++)
        {
            parameters.Enqueue(ParseAddExpression());
            if (i < argumentCount - 1)
                Eat(TokenType.ArgumentDivider);
        }
        Eat(TokenType.CloseParen);

        return _functionProvider.CreateExpression(name, parameters)
            ?? throw new EngineException($"Unexpected Function type: {name}");
    }
}
