namespace CalculatorShell.Engine.Colors;

public record struct YUV
{
    private double _y;
    private double _u;
    private double _v;

    public double Y
    {
        readonly get => _y;
        set => _y = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }

    public double U
    {
        readonly get => _u;
        set => _u = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }

    public double V
    {
        readonly get => _v;
        set => _v = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }
}