//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine.Expressions;

internal sealed class AddExpression : BinaryExpression
{
    public AddExpression(IExpression left, IExpression right) : base(left, right)
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
            return new ConstantExpression(leftConst.Value + rightConst.Value);
        }
        if (leftConst?.Value.IsZero() == true)
        {
            // 0 + y;  return y;
            return newRight;
        }
        if (rightConst?.Value.IsZero() == true)
        {
            // x + 0;  return x;
            return newLeft;
        }
        if (newRight is NegateExpression rightNegate)
        {
            // x + -y;  return x - y;  (this covers -x + -y case too)
            return new SubExpression(newLeft, rightNegate.Child);
        }
        if (newLeft is NegateExpression leftNegate)
        {
            // -x + y;  return y - x;
            return new SubExpression(newRight, leftNegate.Child);
        }
        // x + y;  no simplification
        return new AddExpression(newLeft, newRight);
    }

    public override Expression Compile()
        => Expression.MakeBinary(ExpressionType.Add, Left.Compile(), Right.Compile());

    public override string ToString(CultureInfo cultureInfo)
        => $"({Left.ToString(cultureInfo)} + {Right.ToString(cultureInfo)})";

    protected override Number Evaluate(Number number1, Number number2)
        => number1 + number2;
}
