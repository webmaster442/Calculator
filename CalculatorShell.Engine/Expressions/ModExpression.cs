//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Data;
using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine.Expressions;

internal sealed class ModExpression : BinaryExpression
{
    public ModExpression(IExpression left, IExpression right) : base(left, right)
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
            if (rightConst.Value.IsZero())
            {
                throw new InvalidExpressionException("Divide by zero detected in function");
            }
            return new ConstantExpression(leftConst.Value % rightConst.Value);
        }
        if (leftConst?.Value.IsZero() == true)
        {
            // 0 % y;  return 0;
            if (rightConst?.Value.IsZero() == true)
            {
                throw new InvalidExpressionException("Divide by zero detected in function");
            }
            return new ConstantExpression(new Number((Int128)0));
        }
        if (rightConst != null)
        {
            if (rightConst.Value.IsZero())
            {
                // x / 0;
                throw new InvalidExpressionException("Divide by zero detected in function");
            }
            else if (rightConst.Value.IsOne() || rightConst.Value.IsMinusOne())
            {
                // x % 1;  return 0;
                return new ConstantExpression(new Number(Int128.Zero));
            }
        }

        return new ModExpression(newLeft, newRight);
    }

    public override Expression Compile()
        => Expression.MakeBinary(ExpressionType.Modulo, Left.Compile(), Right.Compile());

    public override string ToString(CultureInfo cultureInfo)
        => $"({Left.ToString(cultureInfo)} % {Right.ToString(cultureInfo)})";

    protected override Number Evaluate(Number number1, Number number2)
        => number1 % number2;
}