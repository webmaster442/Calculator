//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Engine.MathComponents.Stat;

public static class Statistics
{
    public static StatisticsResult Compute(IEnumerable<double> values)
    {
        double[] sorted = values.Order().ToArray();
        StatisticsResult result = new();
        foreach (var value in values)
        {
            result.Sum += value;
        }
        result.Minimum = sorted[0];
        result.Maximum = sorted[^1];
        result.Count = sorted.Length;
        if (result.Count % 2 == 1)
            result.Median = sorted[(sorted.Length - 1) / 2];
        else
            result.Median = sorted[sorted.Length / 2] + sorted[(sorted.Length + 1) / 2];

        return result;
    }
}