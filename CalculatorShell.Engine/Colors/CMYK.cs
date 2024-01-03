namespace CalculatorShell.Engine.Colors;

public record struct CMYK
{
    private double _c;
    private double _m;
    private double _y;
    private double _k;

    public double C
    {
        readonly get => _c;
        set => _c = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }

    public double M
    {
        readonly get => _m; 
        set => _m = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }

    public double Y
    {
        readonly get => _y;
        set => _y = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }

    public double K
    {
        readonly get => _k;
        set => _k = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }
}