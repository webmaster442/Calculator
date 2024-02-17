using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine.Expressions;

internal sealed class Func1Expression : UnaryExpression
{
    private readonly Func1 _function;
    private readonly string _name;

    public Func1Expression(IExpression child, Func1 function, string name) : base(child)
    {
        _function = function;
        _name = name;
    }

    public override IExpression Simplify()
    {
        var newChild = Child.Simplify();

        if (newChild is ConstantExpression childConst)
        {
            // child is constant;  just evaluate it;
            return new ConstantExpression(_function.Evaluate(childConst.Value));
        }
        return new Func1Expression(newChild, _function, _name);
    }

    public override Expression Compile()
    {
        return Expression.Call(Child.Compile(), _function.MethodInfo);
    }

    public override string ToString(CultureInfo cultureInfo)
        => $"{_name}({Child.ToString(cultureInfo)})";

    protected override Number Evaluate(Number number)
        => _function.Evaluate(number);
}
