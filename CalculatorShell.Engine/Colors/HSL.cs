namespace CalculatorShell.Engine.Colors;

public record struct HSL
{
    private double _h;
    private double _s;
    private double _l;

    public double H
    {
        readonly get => _h;
        set => _h = (value > 360) ? 360 : ((value < 0) ? 0 : value);
    }

    public double S
    {
        readonly get => _s;
        set => _s = (value > 1) ? 1 : ((value < 0) ? 0 : value);
    }

    public double L
    {
        readonly get => _l;
        set => _l = (value > 1) ? 1 : ((value < 0) ? 0 : value);
    }
}