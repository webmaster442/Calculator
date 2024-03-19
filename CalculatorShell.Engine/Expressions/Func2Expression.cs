//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine.Expressions;

internal sealed class Func2Expression : BinaryExpression
{
    private readonly Func2 _function;
    private readonly string _name;

    public Func2Expression(IExpression left, IExpression right, Func2 function, string name) : base(left, right)
    {
        _function = function;
        _name = name;
    }

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
            return new Func2Expression(newLeft, newRight, _function, _name);
        }
    }

    public override Expression Compile()
        => Expression.Call(_function.MethodInfo, Left.Compile(), Right.Compile());

    public override string ToString(CultureInfo cultureInfo)
        => $"{_name}({Left.ToString(cultureInfo)}; {Right.ToString(cultureInfo)})";

    protected override Number Evaluate(Number number1, Number number2)
        => _function.Evaluate(number1, number2);
}
