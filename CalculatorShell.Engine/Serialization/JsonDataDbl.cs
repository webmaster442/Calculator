namespace CalculatorShell.Engine.Serialization;

internal sealed class JsonDataDbl : JsonData, INumberConverible
{
    public double Value { get; init; }

    public Number ToNumber() 
        => new(Value);
}
