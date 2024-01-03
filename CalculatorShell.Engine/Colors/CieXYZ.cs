namespace CalculatorShell.Engine.Colors;

public record struct CieXYZ
{
    private double _x;
    private double _y;
    private double _z;

    public double X
    {
        readonly get => _x;
        set => _x = (value > 0.9505) ? 0.9505 : ((value < 0) ? 0 : value);
    }

    public double Y
    {
        readonly get => _y;
        set => _y = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
    }

    public double Z
    {
        readonly get => _z;
        set => _z = (value > 1.089) ? 1.089 : ((value < 0) ? 0 : value);
    }
}