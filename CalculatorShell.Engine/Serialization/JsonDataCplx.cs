using System.Numerics;

namespace CalculatorShell.Engine.Serialization;

internal sealed class JsonDataCplx : JsonData, INumberConverible
{
    public double Real { get; init; }
    public double Imaginary { get; init; }

    public Number ToNumber()
        => new(new Complex(Real, Imaginary));
}
