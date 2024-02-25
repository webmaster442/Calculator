//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine.Expressions;

internal sealed class MultExpression : BinaryExpression
{
    public MultExpression(IExpression left, IExpression right) : base(left, right)
    {
    }

    public override IExpression Simplify()
    {
        var newLeft = Left.Simplify();
        var newRight = Right.Simplify();

        var leftConst = newLeft as ConstantExpression;
        var rightConst = newRight as ConstantExpression;
        var leftNegate = newLeft as NegateExpression;
        var rightNegate = newRight as NegateExpression;

        if (leftConst != null && rightConst != null)
        {
            // two constants;  just evaluate it;
            return new ConstantExpression(leftConst.Value * rightConst.Value);
        }
        if (leftConst != null)
        {
            if (leftConst.Value.IsZero())
            {
                // 0 * y;  return 0;
                return new ConstantExpression(new Number(Int128.Zero));
            }
            if (leftConst.Value.IsOne())
            {
                // 1 * y;  return y;
                return newRight;
            }
            if (leftConst.Value.IsMinusOne())
            {
                // -1 * y;  return -y
                if (rightNegate != null)
                {
                    // y = -u (-y = --u);  return u;
                    return rightNegate.Child;
                }
                return new NegateExpression(newRight);
            }
        }
        else if (rightConst != null)
        {
            if (rightConst.Value.IsZero())
            {
                // x * 0;  return 0;
                return new ConstantExpression(new Number(Int128.Zero));
            }
            if (rightConst.Value.IsOne())
            {
                // x * 1;  return x;
                return newLeft;
            }
            if (rightConst.Value.IsMinusOne())
            {
                // x * -1;  return -x;
                if (leftNegate != null)
                {
                    // x = -u (-x = --u);  return u;
                    return leftNegate.Child;
                }
                return new NegateExpression(newLeft);
            }
        }
        else if (leftNegate != null && rightNegate != null)
        {
            // -x * -y;  return x * y;
            return new MultExpression(leftNegate.Child, rightNegate.Child);
        }
        // x * y;  no simplification
        return new MultExpression(newLeft, newRight);
    }

    public override Expression Compile()
        => Expression.MakeBinary(ExpressionType.Multiply, Left.Compile(), Right.Compile());

    public override string ToString(CultureInfo cultureInfo)
        => $"({Left.ToString(cultureInfo)} * {Right.ToString(cultureInfo)})";

    protected override Number Evaluate(Number number1, Number number2)
        => number1 * number2;
}
