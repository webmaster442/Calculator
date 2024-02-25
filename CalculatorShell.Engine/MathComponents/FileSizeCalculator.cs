using System.Globalization;

namespace CalculatorShell.Engine.MathComponents;
public static class FileSizeCalculator
{
    public const long KiB = 1024;
    public const long MiB = 1024 * KiB;
    public const long GiB = 1024 * MiB;
    public const long TiB = 1024 * GiB;
    public const long PiB = 1024 * TiB;
    public const long EiB = 1024 * PiB;

    public static long ToBytes(double value, string unit)
        => (long)(value * GetUnit(unit));

    public static string ToHumanReadable(long value, CultureInfo cultureInfo)
    {
        FormattableString msg;
        if (value > EiB)
            msg = $"{(double)value / EiB:N3} EiB";
        else if (value > PiB)
            msg = $"{(double)value / PiB:N3} PiB";
        else if(value > TiB)
            msg = $"{(double)value / TiB:N3} TiB";
        else if(value > GiB)
            msg = $"{(double)value / GiB:N3} GiB";
        else if(value > MiB)
            msg = $"{(double)value / MiB:N3} MiB";
        else if(value > KiB)
            msg = $"{(double)value / KiB:N3} KiB";
        else
            msg = $"{value} byte(s)";

        return msg.ToString(cultureInfo);
    }

    private static long GetUnit(string unit)
    {
        return unit.ToLower() switch
        {
            "b" => 1,
            "byte" => 1,
            "bytes" => 1,
            "kib" => KiB,
            "mib" => MiB,
            "gib" => GiB,
            "pib" => PiB,
            "eib" => EiB,
            _ => throw new InvalidOperationException("Unknown unit"),
        };
    }
}
