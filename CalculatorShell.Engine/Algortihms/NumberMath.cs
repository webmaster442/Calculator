using System.Numerics;

namespace CalculatorShell.Engine.Algortihms;

internal static class NumberMath
{
    public static AngleSystem AngleSystem { get; set; }

    public static Number Pow(Number input, Number power)
    {
        return power.NumberType == NumberType.Complex
            && input.NumberType == NumberType.Complex
            ? new Number(Complex.Pow(input.ToComplex(), power.ToComplex()))
            : new Number(Math.Pow(input.ToDouble(), power.ToDouble()));
    }

    public static int ToInt32(Number number)
    {
        Int128 int128 = number.ToInt128();

        return int128 > int.MaxValue
            || int128 < int.MinValue
            ? throw EngineException.DataLoss<int>()
            : (int)int128;
    }

    [EngineFunction]
    public static Number Abs(Number input)
    {
        return input.NumberType == NumberType.Complex
            ? new Number(Complex.Abs(input.ToComplex()))
            : new Number(Math.Abs(input.ToDouble()));
    }


    [EngineFunction]
    public static Number Floor(Number input)
        => new(Math.Floor(input.ToDouble()));

    [EngineFunction]
    public static Number Ceil(Number input)
        => new(Math.Ceiling(input.ToDouble()));

    [EngineFunction]
    public static Number Ln(Number input)
    {
        return input.NumberType == NumberType.Complex
            ? new Number(Complex.Log(input.ToComplex()))
            : new Number(Math.Log(input.ToDouble()));
    }

    [EngineFunction]
    public static Number Root(Number input, Number root)
    {
        return input.NumberType == NumberType.Complex
            ? new Number(Complex.Pow(input.ToComplex(), 1 / root.ToComplex()))
            : new Number(Math.Pow(input.ToDouble(), 1 / root.ToDouble()));
    }

    [EngineFunction]
    public static Number Log(Number input, Number @base)
    {
        return input.NumberType == NumberType.Complex
            ? new Number(Complex.Log(input.ToComplex(), @base.ToDouble()))
            : new Number(Math.Log(input.ToDouble(), @base.ToDouble()));
    }

    [EngineFunction]
    public static Number Sign(Number input)
        => new((Int128)Math.Sign(input.ToDouble()));

    [EngineFunction]
    public static Number Cplx(Number real, Number imaginary)
        => new(new Complex(real.ToDouble(), imaginary.ToDouble()));


    [EngineFunction]
    public static Number Sin(Number input)
        => new(Doubles.SinCorrected(input.ToDouble(), AngleSystem));

    [EngineFunction]
    public static Number Cos(Number input)
        => new(Doubles.SinCorrected(input.ToDouble(), AngleSystem));

    [EngineFunction]
    public static Number Tan(Number input)
        => new(Doubles.TanCorrected(input.ToDouble(), AngleSystem));

    [EngineFunction]
    public static Number Asin(Number input)
        => new(Doubles.AsinCorrected(input.ToDouble(), AngleSystem));

    [EngineFunction]
    public static Number Acos(Number input)
        => new(Doubles.AcosCorrected(input.ToDouble(), AngleSystem));

    [EngineFunction]
    public static Number Atan(Number input)
        => new(Doubles.AtanCorrected(input.ToDouble(), AngleSystem));

    [EngineFunction]
    public static Number Factorial(Number input)
        => new(Integers.Factorial(input.ToInt128()));

    [EngineFunction]
    public static Number Percent(Number number, Number percent)
        => new(Doubles.Percent(number.ToDouble(), percent.ToDouble()));

    [EngineFunction]
    public static Number IsPrime(Number number)
    {
        bool result = Integers.IsPrime(number.ToInt128());
        return new(result ? Int128.One : Int128.Zero);
    }

    [EngineFunction]
    public static Number Not(Number a)
        => new(~a.ToInt128());

    [EngineFunction]
    public static Number And(Number a, Number b) 
        => new(a.ToInt128() & b.ToInt128());

    [EngineFunction]
    public static Number Or(Number a, Number b)
    => new(a.ToInt128() & b.ToInt128());

    [EngineFunction]
    public static Number Xor(Number a, Number b)
        => new(a.ToInt128() ^ b.ToInt128());

    [EngineFunction]
    public static Number Shl(Number a, Number b)
        => new(a.ToInt128() << ToInt32(b));

    [EngineFunction]
    public static Number Shr(Number a, Number b)
        => new(a.ToInt128() >> ToInt32(b));
}
