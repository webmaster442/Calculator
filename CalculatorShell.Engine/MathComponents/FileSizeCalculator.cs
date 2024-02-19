namespace CalculatorShell.Engine.MathComponents;
public static class FileSizeCalculator
{
    private const long KiB = 1024;
    private const long MiB = 1024 * KiB;
    private const long GiB = 1024 * MiB;
    private const long TiB = 1024 * GiB;
    private const long PiB = 1024 * TiB;
    private const long EiB = 1024 * PiB;

    public static long ToBytes(double value, string unit)
        => (long)(value * GetUnit(unit));

    public static string ToHumanReadable(long value)
    {
        if (value > EiB)
            return $"{(double)value / EiB:0.00} EiB";
        if (value > PiB)
            return $"{(double)value / PiB:0.00} PiB";
        if (value > TiB)
            return $"{(double)value / TiB:0.00} TiB";
        if (value > GiB)
            return $"{(double)value / GiB:0.00} GiB";
        if (value > MiB)
            return $"{(double)value / MiB:0.00} MiB";
        if (value > KiB)
            return $"{(double)value / KiB:0.00} KiB";
        else
            return $"{value} byte(s)";
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
