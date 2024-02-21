using System.Diagnostics;

namespace CalculatorShell.Engine;
internal static class TypeDecider
{
    public static NumberType Decide(NumberType left, NumberType right, bool isDivision = false)
    {
        return left switch
        {
            NumberType.Integer => LeftIsInteger(right, isDivision),
            NumberType.Complex => LeftIsComplex(right),
            NumberType.Fraction => LeftIsFraction(right),
            NumberType.Double => LeftIsDouble(right),
            _ => throw new UnreachableException(),
        };
    }

    private static NumberType LeftIsInteger(NumberType right, bool isDivision)
    {
        return right switch
        {
            NumberType.Integer => isDivision ? NumberType.Fraction : NumberType.Integer,
            NumberType.Complex => NumberType.Complex,
            NumberType.Fraction => NumberType.Fraction,
            NumberType.Double => NumberType.Double,
            _ => throw new UnreachableException(),
        };
    }

    private static NumberType LeftIsComplex(NumberType right)
    {
        return right switch
        {
            NumberType.Integer => NumberType.Complex,
            NumberType.Complex => NumberType.Complex,
            NumberType.Fraction => NumberType.Complex,
            NumberType.Double => NumberType.Complex,
            _ => throw new UnreachableException(),
        };
    }

    private static NumberType LeftIsFraction(NumberType right)
    {
        return right switch
        {
            NumberType.Integer => NumberType.Fraction,
            NumberType.Complex => NumberType.Complex,
            NumberType.Fraction => NumberType.Fraction,
            NumberType.Double => NumberType.Double,
            _ => throw new UnreachableException(),
        };
    }

    private static NumberType LeftIsDouble(NumberType right)
    {
        return right switch
        {
            NumberType.Integer => NumberType.Double,
            NumberType.Complex => NumberType.Complex,
            NumberType.Fraction => NumberType.Double,
            NumberType.Double => NumberType.Double,
            _ => throw new UnreachableException(),
        };
    }
}