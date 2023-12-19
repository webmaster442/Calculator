using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

using CalculatorShell.Engine.Algortihms;

namespace CalculatorShell.Engine;

/// <summary>
/// Represents a fractional number
/// </summary>
public struct Fraction :
    IEquatable<Fraction>,
    IComparable<Fraction>,
    IAdditionOperators<Fraction, Fraction, Fraction>,
    ISubtractionOperators<Fraction, Fraction, Fraction>,
    IDivisionOperators<Fraction, Fraction, Fraction>,
    IMultiplyOperators<Fraction, Fraction, Fraction>,
    IModulusOperators<Fraction, Fraction, Fraction>,
    IComparisonOperators<Fraction, Fraction, bool>,
    IAdditiveIdentity<Fraction, Fraction>,
    IMultiplicativeIdentity<Fraction, Fraction>,
    IMinMaxValue<Fraction>,
    IIncrementOperators<Fraction>,
    IDecrementOperators<Fraction>,
    IUnaryPlusOperators<Fraction, Fraction>,
    IUnaryNegationOperators<Fraction, Fraction>,
    IParsable<Fraction>
{
    public static readonly Fraction Zero = new(0, 1);
    public static readonly Fraction One = new(1, 1);

    /// <summary>
    /// The number above the line in a vulgar fraction showing how many of the parts indicated by the denominator are taken
    /// </summary>
    public Int128 Numerator { get; private set; }

    /// <summary>
    /// The bottom number in a fraction that shows the number of equal parts an item is divided into.
    /// </summary>
    public Int128 Denominator { get; private set; }

    /// <inheritdoc/>
    public static Fraction AdditiveIdentity => new(0, 1);

    /// <inheritdoc/>
    public static Fraction MultiplicativeIdentity => new(1, 1);

    /// <inheritdoc/>
    public static Fraction MaxValue => new(Int128.MaxValue, 1);

    /// <inheritdoc/>
    public static Fraction MinValue => new(Int128.MinValue, 1);

    public readonly bool IsInteger => Denominator == 1;

    /// <summary>
    /// Creates a new instance a fractional number
    /// </summary>
    public Fraction()
    {
        Numerator = 0;
        Denominator = 1;
    }
    /// <summary>
    /// Creates a new instance a fractional number
    /// </summary>
    /// <param name="numerator">the numerator</param>
    /// <param name="denominator">the denominator</param>
    /// <exception cref="DivideByZeroException">when the denominator is 0</exception>
    public Fraction(Int128 numerator, Int128 denominator)
    {
        if (denominator == 0)
            throw new DivideByZeroException("0 can't be denominator");

        Numerator = numerator;
        Denominator = denominator;
        Simplify();
    }

    private void Simplify()
    {
        if (Denominator < 0)
        {
            Numerator = -Numerator;
            Denominator = -Denominator;
        }
        Int128 gcd = Integers.Gcd(Numerator, Denominator);
        Numerator /= gcd;
        Denominator /= gcd;
    }

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        return ToString(CultureInfo.InvariantCulture);
    }

    public readonly string ToString(CultureInfo culture)
    {
        if (Denominator == 1)
            return Numerator.ToString(culture);

        return $"{Numerator.ToString(culture)}/{Denominator.ToString(culture)}";
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object? obj)
    {
        return obj is Fraction fraction && Equals(fraction);
    }

    /// <inheritdoc/>
    public readonly bool Equals(Fraction other)
    {
        return Numerator == other.Numerator &&
               Denominator == other.Denominator;
    }

    /// <inheritdoc/>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Numerator, Denominator);
    }

    /// <inheritdoc/>
    public readonly int CompareTo(Fraction other)
    {
        Int128 n1 = Numerator * other.Denominator;
        Int128 n2 = other.Numerator * Denominator;
        return n1.CompareTo(n2);
    }

    public static Fraction Parse(string s, IFormatProvider? provider)
    {
        string[] parts = s.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 1)
            return new Fraction(Int128.Parse(parts[0], provider), 1);
        else if (parts.Length == 2)
            return new Fraction(Int128.Parse(parts[0], provider), Int128.Parse(parts[1], provider));
        else
            throw new FormatException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
    {
        try
        {
            result = Parse(s ?? string.Empty, provider);
            return true;
        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }

    /// <inheritdoc/>
    public static bool operator ==(Fraction left, Fraction right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc/>
    public static bool operator !=(Fraction left, Fraction right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public static Fraction operator +(Fraction left, Fraction right)
    {
        Int128 lcm = Integers.Lcm(left.Denominator, right.Denominator);
        Int128 factorLeft = lcm / left.Denominator;
        Int128 factorRigt = lcm / right.Denominator;
        Int128 numerator = (left.Numerator * factorLeft) + (right.Numerator * factorRigt);
        return new Fraction(numerator, lcm);
    }

    /// <inheritdoc/>
    public static Fraction operator -(Fraction left, Fraction right)
    {
        Int128 lcm = Integers.Lcm(left.Denominator, right.Denominator);
        Int128 factorLeft = lcm / left.Denominator;
        Int128 factorRigt = lcm / right.Denominator;
        Int128 numerator = (left.Numerator * factorLeft) - (right.Numerator * factorRigt);
        return new Fraction(numerator, lcm);
    }

    /// <inheritdoc/>
    public static Fraction operator /(Fraction left, Fraction right)
    {
        Int128 numerator = left.Numerator * right.Denominator;
        Int128 denominator = left.Denominator * right.Numerator;
        return new Fraction(numerator, denominator);
    }

    /// <inheritdoc/>
    public static Fraction operator *(Fraction left, Fraction right)
    {
        Int128 numerator = left.Numerator * right.Numerator;
        Int128 denominator = left.Denominator * right.Denominator;
        return new Fraction(numerator, denominator);
    }

    /// <inheritdoc/>
    public static Fraction operator %(Fraction left, Fraction right)
    {
        Int128 lcm = Integers.Lcm(left.Denominator, right.Denominator);
        Int128 numerator1 = (lcm / left.Denominator) * left.Numerator;
        Int128 numerator2 = (lcm / right.Denominator) * right.Numerator;
        return new Fraction(numerator1 % numerator2, lcm);
    }

    /// <inheritdoc/>
    public static bool operator <(Fraction left, Fraction right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <inheritdoc/>
    public static bool operator <=(Fraction left, Fraction right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <inheritdoc/>
    public static bool operator >(Fraction left, Fraction right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <inheritdoc/>
    public static bool operator >=(Fraction left, Fraction right)
    {
        return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Create a fraction from a long value
    /// </summary>
    /// <param name="number">number</param>
    public static implicit operator Fraction(long number)
    {
        return new Fraction(number, 1);
    }

    /// <inheritdoc/>
    public static Fraction operator ++(Fraction value)
    {
        return value + 1;
    }

    /// <inheritdoc/>
    public static Fraction operator --(Fraction value)
    {
        return value - 1;
    }

    /// <inheritdoc/>
    public static Fraction operator +(Fraction value)
    {
        return new Fraction(+value.Numerator, value.Denominator);
    }

    /// <inheritdoc/>
    public static Fraction operator -(Fraction value)
    {
        return new Fraction(-value.Numerator, value.Denominator);
    }

    public static explicit operator double(Fraction value)
    {
        return (double)value.Numerator / (double)value.Denominator;
    }
}
