using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace CalculatorShell.Engine.Colors;

public record struct CMYK : IParsable<CMYK>
{
    private double _c;
    private double _m;
    private double _y;
    private double _k;

    public double C
    {
        readonly get => _c;
        set => _c = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }

    public double M
    {
        readonly get => _m; 
        set => _m = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }

    public double Y
    {
        readonly get => _y;
        set => _y = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }

    public double K
    {
        readonly get => _k;
        set => _k = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }

    public static CMYK Parse(string s, IFormatProvider? provider)
        => Parsers.ParseCMYK(s, provider);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out CMYK result)
    {
        try
        {
            result = Parsers.ParseCMYK(s ?? string.Empty, provider);
            return true;
        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }

    public readonly string ToString(CultureInfo cultureInfo)
        => $"cmyk({C.ToString(cultureInfo)}, {M.ToString(cultureInfo)}, {Y.ToString(cultureInfo)}, {K.ToString(cultureInfo)})";

    public override readonly string ToString()
        => ToString(CultureInfo.InvariantCulture);
}