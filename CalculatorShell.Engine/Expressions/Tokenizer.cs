using System.Globalization;
using System.Text;

namespace CalculatorShell.Engine.Expressions;

internal sealed class Tokenizer
{
    private readonly string _expression;
    private readonly CultureInfo _culture;
    private readonly HashSet<string> _knownFunctions;

    public Tokenizer(string expression,
                     CultureInfo culture,
                     IEnumerable<string> functions)
    {
        _expression = expression;
        _culture = culture;
        _knownFunctions = functions.ToHashSet(StringComparer.InvariantCultureIgnoreCase);
    }

    public IEnumerable<Token> Tokenize()
    {
        for (int i = 0; i < _expression.Length; i++)
        {
            if (IsIdentifierChar(_expression[i], null, _culture.NumberFormat))
            {
                string identifier = ReadIdentifier(i);

                if (Number.TryParse(identifier, _culture, out Number? parsed))
                {
                    yield return new Token(identifier, TokenType.Constant, parsed);
                }
                else if (_knownFunctions.Contains(identifier))
                {
                    yield return new Token(identifier, TokenType.Function);
                }
                else
                {
                    yield return new Token(identifier, TokenType.Variable);
                }
                i += (identifier.Length - 1);
                continue;
            }
            if (IsComparisionOperatorChar(_expression[i]))
            {
                string comparison = ReadComparisonOperator(i);
                switch (comparison)
                {
                    case "<":
                        yield return new Token("<", TokenType.SmallerThan);
                        break;
                    case ">":
                        yield return new Token(">", TokenType.GreaterThan);
                        break;
                    case "<=":
                        yield return new Token("<=", TokenType.SmallerThanEqual);
                        break;
                    case ">=":
                        yield return new Token(">=", TokenType.GreaterThanEqual);
                        break;
                    case "==":
                        yield return new Token("==", TokenType.Equal);
                        break;
                    case "!=":
                        yield return new Token("!=", TokenType.NotEqual);
                        break;
                    default:
                        throw new EngineException($"Invalid token '{comparison}' in function: {_expression}");
                }
                i += (comparison.Length -1);
                continue;
            }

            switch (_expression[i])
            {
                case ' ':
                case '\t':
                case '\r':
                case '\n':
                    continue;
                case '+':
                    yield return new Token("+", TokenType.Plus);
                    break;
                case '-':
                    yield return new Token("-", TokenType.Minus);
                    break;
                case '*':
                    yield return new Token("*", TokenType.Multiply);
                    break;
                case '/':
                    yield return new Token("/", TokenType.Divide);
                    break;
                case '%':
                    yield return new Token("%", TokenType.Mod);
                    break;
                case '^':
                    yield return new Token("^", TokenType.Exponent);
                    break;
                case '(':
                    yield return new Token("(", TokenType.OpenParen);
                    break;
                case ')':
                    yield return new Token(")", TokenType.CloseParen);
                    break;
                case ';':
                    yield return new Token(";", TokenType.ArgumentDivider);
                    break;
                default:
                    throw new EngineException($"Invalid token '{_expression[i]}' in function: {_expression}");
            }

        }
        yield return new Token(string.Empty, TokenType.Eof, null);
    }

    private string ReadComparisonOperator(int startIndex)
    {
        StringBuilder op = new();
        int i = startIndex;
        while (i < _expression.Length
            && IsComparisionOperatorChar(_expression[i]))
        {
            op.Append(_expression[i]);
            ++i;
        }
        return op.ToString();
    }

    private string ReadIdentifier(int startIndex)
    {
        StringBuilder identifier = new();
        int i = startIndex;
        char? previous = i - 1 > 0 ? _expression[i - 1] : null;
        while (i < _expression.Length
            && IsIdentifierChar(_expression[i], previous, _culture.NumberFormat))
        {
            identifier.Append(_expression[i]);
            ++i;
            previous = _expression[i - 1];
        }
        return identifier.ToString();
    }

    private static bool IsComparisionOperatorChar(char c)
    {
        return c == '<'
            || c == '>'
            || c == '='
            || c == '!';
    }

    private static bool IsIdentifierChar(char c, char? prev, NumberFormatInfo numberFormatInfo)
    {
        return
            numberFormatInfo.NumberDecimalSeparator.Any(x => x == c)
            || c == '_'
            || ('a' <= c && c <= 'z')
            || ('A' <= c && c <= 'Z')
            || ('0' <= c && c <= '9')
            || (prev != null && (prev == 'e' || prev == 'E') && c == '-');
    }
}
