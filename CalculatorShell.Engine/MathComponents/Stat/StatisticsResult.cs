namespace CalculatorShell.Engine.MathComponents.Stat;

public sealed class StatisticsResult
{
    public double Minimum { get; set; }
    public double Maximum { get; set; }
    public long Count { get; set; }
    public double Sum { get; set; }
    public double Mean => Sum / Count;
    public double Median { get; set; }
    public double Range => Maximum - Minimum;

    public override string ToString()
    {
        return $"""
            Count:   {Count}
            Minimum: {Minimum}
            Maximum: {Maximum}
            Sum:     {Sum}
            Mean:    {Mean}
            Median:  {Median}
            Range:   {Range}
            """;
    }
}
