using System.Globalization;
using System.Text;

namespace CalculatorShell.Engine.Expressions;

internal sealed class Tokenizer
{
    private readonly string _function;
    private readonly CultureInfo _culture;
    private readonly HashSet<string> _knownFunctions;
    private int _index;

    public Tokenizer(string expression,
                     CultureInfo culture,
                     IEnumerable<string> functions)
    {
        _function = expression;
        _culture = culture;
        _knownFunctions = functions.ToHashSet(StringComparer.InvariantCultureIgnoreCase);
        _index = 0;
    }

    public IEnumerable<Token> Tokenize()
    {
        while (_index < _function.Length)
        {
            if (IsIdentifierChar(_function[_index], null, _culture.NumberFormat))
            {
                StringBuilder identifier = new();
                identifier.Append(_function[_index++]);
                while (_index < _function.Length
                    && IsIdentifierChar(_function[_index], _function[_index - 1], _culture.NumberFormat))
                {
                    identifier.Append(_function[_index++]);
                }

                string id = identifier.ToString();

                if (Number.TryParse(id, _culture, out Number? parsed))
                {
                    yield return new Token(id, TokenType.Constant, parsed);
                }
                else if (_knownFunctions.Contains(id))
                {
                    yield return new Token(id, TokenType.Function);
                }
                else
                {
                    yield return new Token(id, TokenType.Variable);
                }
            }

            int nextIndex = _index++;

            if (nextIndex < _function.Length)
            {
                switch (_function[nextIndex])
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
                        throw new EngineException($"Invalid token '{_function[_index - 1]}' in function: {_function}");
                }
            }
        }
        yield return new Token("", TokenType.Eof);
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
