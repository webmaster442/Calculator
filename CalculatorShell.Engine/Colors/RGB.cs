using System.Diagnostics.CodeAnalysis;

namespace CalculatorShell.Engine.Colors;

public record struct RGB : IParsable<RGB>
{
    private int _r;
    private int _g;
    private int _b;

    public int R
    {
        readonly get => _r;
        set => _r = (value > 255) ? 255 : ((value < 0) ? 0 : value);
    }

    public int G
    {
        readonly get => _g;
        set => _g = (value > 255) ? 255 : ((value < 0) ? 0 : value);
    }

    public int B
    {
        readonly get => _b;
        set => _b = (value > 255) ? 255 : ((value < 0) ? 0 : value);
    }

    public static RGB Parse(string s, IFormatProvider? provider) 
        => Parsers.ParseRGB(s, provider);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out RGB result)
    {
        try
        {
            result = Parsers.ParseRGB(s ?? string.Empty, provider);
            return true;
        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }
}
