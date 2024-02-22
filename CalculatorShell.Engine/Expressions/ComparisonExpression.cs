using System.Reflection;

namespace CalculatorShell.Engine.Expressions;

internal abstract class ComparisonExpression : BinaryExpression
{
    protected ComparisonExpression(IExpression left, IExpression right) : base(left, right)
    {
        _castMethod = typeof(ComparisonExpression).GetMethod(nameof(Cast), BindingFlags.Static | BindingFlags.NonPublic) 
            ?? throw new InvalidOperationException("Can't find cast");
    }

    protected readonly MethodInfo _castMethod;

    private static Number Cast(bool value)
    {
        if (value)
            return Number.FromInteger(1);

        return Number.FromInteger(0);
    }
}
