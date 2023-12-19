using System.Globalization;

using CalculatorShell.Engine.Algortihms;

namespace CalculatorShell.Engine.Expressions;

internal sealed class ExpExpression : BinaryExpression
{
    public ExpExpression(IExpression left, IExpression right) : base(left, right)
    {
    }

    public override IExpression Simplify()
    {
        var newLeft = Left.Simplify();
        var newRight = Right.Simplify();

        var leftConst = newLeft as ConstantExpression;
        var rightConst = newRight as ConstantExpression;

        if (leftConst != null && rightConst != null)
        {
            // two constants;  just evaluate it;
            return new ConstantExpression(NumberMath.Pow(leftConst.Value, rightConst.Value));
        }
        if (rightConst != null)
        {
            if (rightConst.Value.IsZero())
            {
                // x ^ 0;  return 1;
                return new ConstantExpression(new Number(Int128.One));
            }
            if (rightConst.Value.IsOne())
            {
                // x ^ 1;  return x;
                return newLeft;
            }
        }
        else if (leftConst?.Value.IsZero() == true)
        {
            // 0 ^ y;  return 0;
            return new ConstantExpression(new Number(Int128.Zero));
        }
        // x ^ y;  no simplification
        return new ExpExpression(newLeft, newRight);
    }

    public override string ToString(CultureInfo cultureInfo)
    {
        return $"({Left.ToString(cultureInfo)} ^ {Right.ToString(cultureInfo)})";
    }

    protected override Number Evaluate(Number number1, Number number2)
    {
        return NumberMath.Pow(number1, number2);
    }
}
