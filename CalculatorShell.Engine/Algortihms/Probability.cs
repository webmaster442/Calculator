//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Engine.Algortihms;

internal static class Probability
{
    private static (double n, double k, double nk) Factorial(int n, int k)
    {
        int destination = Math.Max(n, k);

        double factorial = 1;

        (double n, double k, double nk) result = new(1.0, 1.0, 1.0);

        for (int i = 1; i <= destination; i++)
        {
            factorial *= i;
            if (i == n)
                result.n = factorial;
            if (i == k)
                result.k = factorial;
            if (i == n - k)
                result.nk = factorial;
        }

        return result;
    }

    public static double Binomial(Int128 n, Int128 k)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(k, n);
        ArgumentOutOfRangeException.ThrowIfLessThan(n, 0);
        ArgumentOutOfRangeException.ThrowIfLessThan(k, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(n, 100);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(k, 100);
        var factors = Factorial((int)n, (int)k);
        return factors.n / (factors.k * factors.nk);
    }
}
