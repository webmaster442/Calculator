namespace CalculatorShell.Engine.Colors;

public record struct RGB
{
    private int _r;
    private int _g;
    private int _b;

    public int R
    {
        readonly get => _r;
        set => _r = (value > 255) ? 255 : ((value < 0) ? 0 : value);
    }

    public int G
    {
        readonly get => _g;
        set => _g = (value > 255) ? 255 : ((value < 0) ? 0 : value);
    }

    public int B
    {
        readonly get => _b;
        set => _b = (value > 255) ? 255 : ((value < 0) ? 0 : value);
    }
}
