using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace CalculatorShell.Engine.Colors;

public record struct HSL : IParsable<HSL>
{
    private double _h;
    private double _s;
    private double _l;

    public double H
    {
        readonly get => _h;
        set => _h = (value > 360.0) ? 360.0 : ((value < 0.0) ? 0.0 : value);
    }

    public double S
    {
        readonly get => _s;
        set => _s = (value > 1.0) ? 1.0 : ((value < 0.0) ? 0.0 : value);
    }

    public double L
    {
        readonly get => _l;
        set => _l = (value > 1.0) ? 1.0 : ((value < 0.0) ? 0.0 : value);
    }

    public static HSL Parse(string s, IFormatProvider? provider)
        => Parsers.ParseHSL(s, provider);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out HSL result)
    {
        try
        {
            result = Parsers.ParseHSL(s ?? string.Empty, provider);
            return true;
        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }

    public readonly string ToString(CultureInfo cultureInfo)
        => $"hsl({H.ToString(cultureInfo)}, {S.ToString(cultureInfo)}, {L.ToString(cultureInfo)})";

    public override readonly string ToString()
        => ToString(CultureInfo.InvariantCulture);
}