namespace CalculatorShell.Engine.Expressions;

internal sealed class TokenSet
{
    private readonly uint _tokens;

    public TokenSet(TokenType type)
    {
        _tokens = (uint)type;
    }

    private TokenSet(uint tokens)
    {
        _tokens = tokens;
    }

    public TokenSet(TokenSet t)
    {
        _tokens = t._tokens;
    }

    public static TokenSet operator +(TokenSet t1, TokenSet t2)
        => new(t1._tokens | t2._tokens);

    public static TokenSet operator +(TokenSet t1, TokenType t2)
        => new(t1._tokens | (uint)t2);

    public bool Contains(TokenType type)
        => (_tokens & (uint)type) != 0;
}
