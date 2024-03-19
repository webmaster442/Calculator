//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine.Expressions;

internal sealed class DivExpression : BinaryExpression
{
    public DivExpression(IExpression left, IExpression right) : base(left, right)
    {
    }

    public override IExpression Simplify()
    {
        var newLeft = Left.Simplify();
        var newRight = Right.Simplify();

        var leftConst = newLeft as ConstantExpression;
        var rightConst = newRight as ConstantExpression;
        var leftNegate = newLeft as NegateExpression;

        if (leftConst != null && rightConst != null)
        {
            // two constants;  just evaluate it;
            return rightConst.Value.IsZero()
                ? throw new EngineException("Divide by zero detected in function")
                : (IExpression)new ConstantExpression(leftConst.Value / rightConst.Value);
        }
        if (leftConst?.Value.IsZero() == true)
        {
            // 0 / y;  return 0;
            return rightConst?.Value.IsZero() == true
                ? throw new EngineException("Divide by zero detected in function")
                : (IExpression)new ConstantExpression(new Number((Int128)0));
        }
        if (rightConst != null)
        {
            if (rightConst.Value.IsZero())
            {
                // x / 0;
                throw new EngineException("Divide by zero detected in function");
            }
            if (rightConst.Value.IsOne())
            {
                // x / 1;  return x;
                return newLeft;
            }
            if (rightConst.Value.IsMinusOne())
            {
                // x / -1;  return -x;
                if (leftNegate != null)
                {
                    // x = -u (-x = --u);  return u;
                    return leftNegate.Child;
                }
                return new NegateExpression(newLeft);
            }
        }
        else if (leftNegate != null && newRight is NegateExpression rightNegate)
        {
            // -x / -y;  return x / y;
            return new DivExpression(leftNegate.Child, rightNegate.Child);
        }
        // x / y;  no simplification
        return new DivExpression(newLeft, newRight);
    }

    public override Expression Compile()
        => Expression.MakeBinary(ExpressionType.Divide, Left.Compile(), Right.Compile());

    public override string ToString(CultureInfo cultureInfo)
        => $"({Left.ToString(cultureInfo)} / {Right.ToString(cultureInfo)})";

    protected override Number Evaluate(Number number1, Number number2)
        => number1 / number2;
}
