//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine.Expressions;

internal class LessThanExpression : ComparisonExpression
{
    public LessThanExpression(IExpression left, IExpression right) : base(left, right)
    {
    }

    public override Expression Compile()
        => Expression.Call(_castMethod, Expression.MakeBinary(ExpressionType.LessThan, Left.Compile(), Right.Compile()));

    public override IExpression Simplify()
    {
        IExpression newLeft = Left.Simplify();
        IExpression newRight = Right.Simplify();

        if (newLeft is ConstantExpression leftConst
            && newRight is ConstantExpression rightConst)
        {
            // two constants
            return new ConstantExpression(Evaluate(leftConst.Value, rightConst.Value));
        }
        else
        {
            return new LessThanExpression(Left, Right);
        }
    }

    public override string ToString(CultureInfo cultureInfo)
        => $"({Left.ToString(cultureInfo)} < {Right.ToString(cultureInfo)})";

    protected override Number Evaluate(Number number1, Number number2)
        => number1 < number2 ? Number.FromInteger(1) : Number.FromInteger(0);
}
