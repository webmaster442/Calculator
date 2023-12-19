using System.Globalization;

namespace CalculatorShell.Engine.Expressions;

internal sealed class SingleFunctionExpression : UnaryExpression
{
    private readonly SingleParamFunction _function;
    private readonly string _name;

    public SingleFunctionExpression(IExpression child, SingleParamFunction function, string name) : base(child)
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
            return new ConstantExpression(_function.Invoke(childConst.Value));
        }
        return new SingleFunctionExpression(newChild, _function, _name);
    }

    public override string ToString(CultureInfo cultureInfo)
        => $"{_name}({Child.ToString(cultureInfo)})";

    protected override Number Evaluate(Number number)
        => _function.Invoke(number);
}
