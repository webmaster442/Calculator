namespace CalculatorShell.Engine.Serialization;

internal sealed class JsonDataInt : JsonData, INumberConverible
{
    public Int128 Value { get; init; }

    public Number ToNumber()
        => new(Value);
}
