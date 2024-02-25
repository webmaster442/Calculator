//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine.Expressions;

internal sealed class NegateExpression : UnaryExpression
{
    public NegateExpression(IExpression child) : base(child)
    {
    }

    public override IExpression Simplify()
    {
        var newChild = Child.Simplify();

        if (newChild is ConstantExpression childConst)
        {
            // child is constant;  just evaluate it;
            return new ConstantExpression(-childConst.Value);
        }
        return new NegateExpression(newChild);
    }

    public override Expression Compile() 
        => Expression.Negate(Child.Compile());

    public override string ToString(CultureInfo cultureInfo)
    {
        return $"(-{Child.ToString(cultureInfo)})";
    }

    protected override Number Evaluate(Number number)
        => -number;
}
