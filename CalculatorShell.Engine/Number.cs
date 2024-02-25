//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

using CalculatorShell.Engine.Expressions;

namespace CalculatorShell.Engine;

public class Number :
    IAdditionOperators<Number, Number, Number>,
    ISubtractionOperators<Number, Number, Number>,
    IMultiplyOperators<Number, Number, Number>,
    IDivisionOperators<Number, Number, Number>,
    IUnaryNegationOperators<Number, Number>,
    IComparisonOperators<Number, Number, bool>,
    IEquatable<Number?>
{
    private readonly Complex _complex;
    private readonly Fraction _fraction;
    private readonly double _double;
    private readonly Int128 _int128;

    public NumberType NumberType { get; }

    public Number(Complex complex)
    {
        _complex = complex;
        NumberType = NumberType.Complex;
    }

    public Number(Fraction fraction)
    {
        _fraction = fraction;
        NumberType = NumberType.Fraction;
    }

    public Number(double @double)
    {
        _double = @double;
        NumberType = NumberType.Double;
    }

    public Number(Int128 int128)
    {
        _int128 = int128;
        NumberType = NumberType.Integer;
    }

    public static Number FromInteger(long n)
        => new Number((Int128)n);

    internal Number(JsonNumber number)
    {
        NumberType = Enum.Parse<NumberType>(number.Type);
        switch (NumberType)
        {
            case NumberType.Complex:
                _complex = Complex.Parse(number.Value, CultureInfo.InvariantCulture);
                break;
            case NumberType.Fraction:
                _fraction = Fraction.Parse(number.Value, CultureInfo.InvariantCulture);
                break;
            case NumberType.Double:
                _double = Double.Parse(number.Value, CultureInfo.InvariantCulture);
                break;
            case NumberType.Integer:
                _int128 = Int128.Parse(number.Value, CultureInfo.InvariantCulture);
                break;
            default:
                throw new UnreachableException();
        }
    }

    public Complex ToComplex()
    {
        return NumberType switch
        {
            NumberType.Complex => _complex,
            NumberType.Double => new Complex(_double, 0),
            NumberType.Fraction => new Complex(_fraction.ToDouble(), 0),
            NumberType.Integer => new Complex(_int128.ToDouble(), 0),
            _ => throw new UnreachableException(),
        };
    }

    public Fraction ToFraction()
    {
        return NumberType switch
        {
            NumberType.Fraction => _fraction,
            NumberType.Integer => new Fraction(_int128, 1),
            _ => throw EngineException.CreateTypeMismatch<Fraction>(NumberType),
        };
    }

    public Int128 ToInt128()
    {
        return NumberType switch
        {
            NumberType.Integer => _int128,
            NumberType.Fraction => _fraction.IsInteger ? _fraction.Numerator : throw EngineException.CreateTypeMismatch<Int128>(NumberType),
            _ => throw EngineException.CreateTypeMismatch<Int128>(NumberType),
        };
    }

    public double ToDouble()
    {
        return NumberType switch
        {
            NumberType.Double => _double,
            NumberType.Fraction => _fraction.ToDouble(),
            NumberType.Integer => _int128.ToDouble(),
            _ => throw EngineException.CreateTypeMismatch<double>(NumberType),
        };
    }

    public bool IsZero()
    {
        return NumberType switch
        {
            NumberType.Integer => _int128 == 0,
            NumberType.Fraction => _fraction == 0,
            NumberType.Double => Math.Abs(_double) < 1E-15,
            NumberType.Complex => _complex == Complex.Zero,
            _ => throw new UnreachableException(),
        };
    }

    public bool IsOne()
    {
        return NumberType switch
        {
            NumberType.Integer => _int128 == 1,
            NumberType.Fraction => _fraction == 1,
            NumberType.Double => 1.0 - Math.Abs(_double) < 1E-15,
            NumberType.Complex => _complex == Complex.One,
            _ => throw new UnreachableException(),
        };
    }


    public bool IsMinusOne()
    {
        return NumberType switch
        {
            NumberType.Integer => _int128 == -1,
            NumberType.Fraction => _fraction == -1,
            NumberType.Double => -1.0 - Math.Abs(_double) < 1E-15,
            NumberType.Complex => _complex == new Complex(-1, 0),
            _ => throw new UnreachableException(),
        };
    }

    private static Number Parse(string? s, IFormatProvider? provider)
    {
        if (IntegerParser.TryParse(s, provider, out Int128 int128))
            return new Number(int128);
        else if (double.TryParse(s, provider, out double doubleValue))
            return new Number(doubleValue);

        throw EngineException.CreateNumberParse(s);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Number result)
    {
        try
        {
            result = Parse(s, provider);
            return true;
        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }

    public static Number operator +(Number left, Number right)
    {
        return TypeDecider.Decide(left.NumberType, right.NumberType) switch
        {
            NumberType.Complex => new Number(left.ToComplex() + right.ToComplex()),
            NumberType.Fraction => new Number(left.ToFraction() + right.ToFraction()),
            NumberType.Double => new Number(left.ToDouble() + right.ToDouble()),
            NumberType.Integer => new Number(left.ToInt128() + right.ToInt128()),
            _ => throw new UnreachableException(),
        };
    }

    public static Number operator -(Number left, Number right)
    {
        return TypeDecider.Decide(left.NumberType, right.NumberType) switch
        {
            NumberType.Complex => new Number(left.ToComplex() - right.ToComplex()),
            NumberType.Fraction => new Number(left.ToFraction() - right.ToFraction()),
            NumberType.Double => new Number(left.ToDouble() - right.ToDouble()),
            NumberType.Integer => new Number(left.ToInt128() - right.ToInt128()),
            _ => throw new UnreachableException(),
        };
    }

    public static Number operator *(Number left, Number right)
    {
        return TypeDecider.Decide(left.NumberType, right.NumberType) switch
        {
            NumberType.Complex => new Number(left.ToComplex() * right.ToComplex()),
            NumberType.Fraction => new Number(left.ToFraction() * right.ToFraction()),
            NumberType.Double => new Number(left.ToDouble() * right.ToDouble()),
            NumberType.Integer => new Number(left.ToInt128() * right.ToInt128()),
            _ => throw new UnreachableException(),
        };
    }

    public static Number operator /(Number left, Number right)
    {
        return TypeDecider.Decide(left.NumberType, right.NumberType, isDivision: true) switch
        {
            NumberType.Complex => new Number(left.ToComplex() / right.ToComplex()),
            NumberType.Fraction => new Number(left.ToFraction() / right.ToFraction()),
            NumberType.Double => new Number(left.ToDouble() / right.ToDouble()),
            NumberType.Integer => new Number(left.ToInt128() / right.ToInt128()),
            _ => throw new UnreachableException(),
        };
    }

    public static Number operator %(Number left, Number right)
    {
        return TypeDecider.Decide(left.NumberType, right.NumberType, isDivision: true) switch
        {
            NumberType.Complex => throw EngineException.CreateArithmetic(left, right, "%"),
            NumberType.Fraction => new Number(left.ToFraction() % right.ToFraction()),
            NumberType.Double => new Number(left.ToDouble() % right.ToDouble()),
            NumberType.Integer => new Number(left.ToInt128() % right.ToInt128()),
            _ => throw new UnreachableException(),
        };
    }

    public static Number operator -(Number value)
    {
        return value.NumberType switch
        {
            NumberType.Complex => new Number(value._complex * -1.0),
            NumberType.Fraction => new Number(-value._fraction),
            NumberType.Double => new Number(-value._double),
            NumberType.Integer => new Number(-value._int128),
            _ => throw new UnreachableException(),
        };
    }

    public static bool operator >(Number left, Number right)
    {
        return TypeDecider.Decide(left.NumberType, right.NumberType) switch
        {
            NumberType.Complex => throw EngineException.CreateArithmetic(left, right, ">"),
            NumberType.Fraction => left.ToFraction() > right.ToFraction(),
            NumberType.Double => left.ToDouble() > right.ToDouble(),
            NumberType.Integer => left.ToInt128() > right.ToInt128(),
            _ => throw new UnreachableException(),
        };
    }

    public static bool operator >=(Number left, Number right)
    {
        return TypeDecider.Decide(left.NumberType, right.NumberType) switch
        {
            NumberType.Complex => throw EngineException.CreateArithmetic(left, right, ">="),
            NumberType.Fraction => left.ToFraction() >= right.ToFraction(),
            NumberType.Double => left.ToDouble() >= right.ToDouble(),
            NumberType.Integer => left.ToInt128() >= right.ToInt128(),
            _ => throw new UnreachableException(),
        };
    }

    public static bool operator <(Number left, Number right)
    {
        return TypeDecider.Decide(left.NumberType, right.NumberType) switch
        {
            NumberType.Complex => throw EngineException.CreateArithmetic(left, right, "<"),
            NumberType.Fraction => left.ToFraction() < right.ToFraction(),
            NumberType.Double => left.ToDouble() < right.ToDouble(),
            NumberType.Integer => left.ToInt128() < right.ToInt128(),
            _ => throw new UnreachableException(),
        };
    }

    public static bool operator <=(Number left, Number right)
    {
        return TypeDecider.Decide(left.NumberType, right.NumberType) switch
        {
            NumberType.Complex => throw EngineException.CreateArithmetic(left, right, "<="),
            NumberType.Fraction => left.ToFraction() <= right.ToFraction(),
            NumberType.Double => left.ToDouble() <= right.ToDouble(),
            NumberType.Integer => left.ToInt128() <= right.ToInt128(),
            _ => throw new UnreachableException(),
        };
    }

    public static bool operator ==(Number? left, Number? right)
    {
        return left?.Equals(right) == true;
    }

    public static bool operator !=(Number? left, Number? right)
    {
        return left?.Equals(right) == false;
    }

    public override string ToString()
    {
        return ToString(CultureInfo.InvariantCulture);
    }

    public string ToString(CultureInfo culture)
    {
        return NumberType switch
        {
            NumberType.Complex => ComplexToString(culture),
            NumberType.Fraction => _fraction.ToString(culture),
            NumberType.Double => _double.ToString(culture),
            NumberType.Integer => _int128.ToString(culture),
            _ => throw new UnreachableException(),
        };
    }

    private string ComplexToString(CultureInfo culture)
    {
        return $"real: {_complex.Real.ToString(culture)} imaginary: {_complex.Imaginary.ToString(culture)}";
    }

    internal JsonNumber ToJsonNumber()
    {
        string GetRawString()
        {
            return NumberType switch
            {
                NumberType.Complex => _complex.ToString(CultureInfo.InvariantCulture),
                NumberType.Fraction => _fraction.ToString(CultureInfo.InvariantCulture),
                NumberType.Double => _double.ToString(CultureInfo.InvariantCulture),
                NumberType.Integer => _int128.ToString(CultureInfo.InvariantCulture),
                _ => throw new UnreachableException(),
            };
        }

        return new JsonNumber
        {
            Type = NumberType.ToString(),
            Value = GetRawString(),
        };
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Number);
    }

    public bool Equals(Number? other)
    {

        if (object.ReferenceEquals(other, null))
            return false;

        return TypeDecider.Decide(this.NumberType, other.NumberType) switch
        {
            NumberType.Complex => this.ToComplex() == other.ToComplex(),
            NumberType.Fraction => this.ToFraction() == other.ToFraction(),
            NumberType.Double => this.ToDouble() == other.ToDouble(),
            NumberType.Integer => this.ToInt128() == other.ToInt128(),
            _ => throw new UnreachableException(),
        };
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_complex,
                                _fraction,
                                _double,
                                _int128,
                                NumberType);
    }
}
