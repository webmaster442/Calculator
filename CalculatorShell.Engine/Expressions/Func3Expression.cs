//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine.Expressions;

internal sealed class Func3Expression : IExpression
{
    private readonly IExpression _a1;
    private readonly IExpression _a2;
    private readonly IExpression _a3;
    private readonly Func3 _function;
    private readonly string _name;

    public Func3Expression(IExpression a1, IExpression a2, IExpression a3, Func3 function, string name) 
    {
        _a1 = a1;
        _a2 = a2;
        _a3 = a3;
        _function = function;
        _name = name;
    }

    public IExpression Simplify()
    {
        IExpression na1 = _a1.Simplify();
        IExpression na2 = _a2.Simplify();
        IExpression na3 = _a3.Simplify();

        if (na1 is ConstantExpression leftConst
            && na2 is ConstantExpression rightConst
            && na3 is ConstantExpression lastConst)
        {
            // two constants
            return new ConstantExpression(Evaluate(leftConst.Value, rightConst.Value, lastConst.Value));
        }
        else
        {
            return new Func3Expression(na1, na2, na3, _function, _name);
        }
    }

    public Expression Compile()
        => Expression.Call(_function.MethodInfo, _a1.Compile(), _a2.Compile(), _a3.Compile());

    public string ToString(CultureInfo cultureInfo)
        => $"{_name}({_a1.ToString(cultureInfo)}; {_a2.ToString(cultureInfo)}, {_a3.ToString(cultureInfo)})";

    private Number Evaluate(Number number1, Number number2, Number number3)
        => _function.Evaluate(number1, number2, number3);

    public Number Evaluate()
        => Evaluate(_a1.Evaluate(), _a2.Evaluate(), _a3.Evaluate());
}