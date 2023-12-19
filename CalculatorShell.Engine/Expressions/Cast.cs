namespace CalculatorShell.Engine.Expressions;

internal static class Cast
{
    public static double ToDouble(this Fraction fraction)
    {
        if (fraction.IsInteger)
        {
            if (fraction > long.MaxValue
                || fraction < long.MinValue)
            {
                throw EngineException.DataLoss<double>();
            }
        }
        return (double)fraction;
    }

    public static double ToDouble(this Int128 int128)
    {
        return int128 > long.MaxValue
            || int128 < long.MinValue
            ? throw EngineException.DataLoss<double>()
            : (double)int128;
    }
}
