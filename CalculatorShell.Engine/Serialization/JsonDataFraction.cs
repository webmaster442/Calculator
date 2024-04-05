namespace CalculatorShell.Engine.Serialization;

internal sealed class JsonDataFraction : JsonData, INumberConverible
{
    public Int128 Numerator { get; init; }
    public Int128 Denumerator { get; init; }

    public Number ToNumber() 
        => new(new Fraction(Numerator, Denumerator));
}