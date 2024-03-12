//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

namespace CalculatorShell.Engine.MathComponents.Stat;

public sealed class StatisticsResult : ICalculatorFormattable
{
    public double Minimum { get; set; }
    public double Maximum { get; set; }
    public long Count { get; set; }
    public double Sum { get; set; }
    public double Mean => Sum / Count;
    public double Median { get; set; }
    public double Range => Maximum - Minimum;

    public string ToString(CultureInfo culture, bool thousandsGrouping)
    {
        return $"""
            Count:   {NumberFomatter.ToString(Count, culture, thousandsGrouping)}
            Minimum: {NumberFomatter.ToString(Minimum, culture, thousandsGrouping)}
            Maximum: {NumberFomatter.ToString(Maximum, culture, thousandsGrouping)}
            Sum:     {NumberFomatter.ToString(Sum, culture, thousandsGrouping)}
            Mean:    {NumberFomatter.ToString(Mean, culture, thousandsGrouping)}
            Median:  {NumberFomatter.ToString(Median, culture, thousandsGrouping)}
            Range:   {NumberFomatter.ToString(Range, culture, thousandsGrouping)}
            """;
    }
}
