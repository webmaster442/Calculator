using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace CalculatorShell.Engine.Colors;

public record struct CieXYZ : IParsable<CieXYZ>
{
    private double _x;
    private double _y;
    private double _z;

    public double X
    {
        readonly get => _x;
        set => _x = (value > 0.9505) ? 0.9505 : ((value < 0) ? 0 : value);
    }

    public double Y
    {
        readonly get => _y;
        set => _y = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }

    public double Z
    {
        readonly get => _z;
        set => _z = (value > 1.089) ? 1.089 : ((value < 0) ? 0 : value);
    }

    public static CieXYZ Parse(string s, IFormatProvider? provider)
        => Parsers.ParseCieXYZ(s, provider);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out CieXYZ result)
    {
        try
        {
            result = Parsers.ParseCieXYZ(s ?? string.Empty, provider);
            return true;
        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }

    public readonly string ToString(CultureInfo cultureInfo)
        => $"xyz({X.ToString(cultureInfo)}, {Y.ToString(cultureInfo)}, {Z.ToString(cultureInfo)})";

    public override readonly string ToString()
        => ToString(CultureInfo.InvariantCulture);
}