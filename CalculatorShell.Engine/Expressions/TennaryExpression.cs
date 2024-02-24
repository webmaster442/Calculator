using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace CalculatorShell.Engine.Expressions;

internal class TennaryExpression : IExpression
{
    private readonly IExpression _check;
    private readonly IExpression _true;
    private readonly IExpression _false;
    private readonly MethodInfo _castMethod;

    public TennaryExpression(IExpression check, IExpression @true, IExpression @false)
    {
        _check = check;
        _true = @true;
        _false = @false;
        _castMethod = typeof(TennaryExpression).GetMethod(nameof(Cast), BindingFlags.Static | BindingFlags.NonPublic)
            ?? throw new InvalidOperationException("Can't find cast");
    }

    private static bool Cast(Number number)
    {
        if (number.IsOne())
            return true;
        return false;
    }

    public Expression Compile()
    {
        var check = Expression.Call(_castMethod, _check.Compile());
        return Expression.Condition(check, _true.Compile(), _false.Compile());
    }

    public Number Evaluate()
    {
        if (_check.Evaluate() == Number.FromInteger(1))
            return _true.Evaluate();
        else
            return _false.Evaluate();
    }

    public IExpression Simplify()
    {
        if (_check.Simplify() is ConstantExpression constant)
        {
            if (constant.Value == Number.FromInteger(1))
                return _true.Simplify();
            else
                return _false.Simplify();
        }

        return new TennaryExpression(_check, _true, _false);

    }

    public string ToString(CultureInfo cultureInfo)
        => $"({_check.ToString(cultureInfo)}) ? ({_true.ToString(cultureInfo)}) : ({_false.ToString(cultureInfo)})";

    public override string ToString()
        => ToString(CultureInfo.InvariantCulture);
}
