using System.Globalization;

namespace CalculatorShell.Engine.Expressions;

internal sealed partial class DoubleFunctionExpression : BinaryExpression
{
    private readonly DoubleParamFunction _function;
    private readonly string _name;

    public DoubleFunctionExpression(IExpression left, IExpression right, DoubleParamFunction function, string name) : base(left, right)
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
            return new DoubleFunctionExpression(newLeft, newRight, _function, _name);
        }

    }

    public override string ToString(CultureInfo cultureInfo)
        => $"{_name}({Left.ToString(cultureInfo)}; {Right.ToString(cultureInfo)})";

    protected override Number Evaluate(Number number1, Number number2)
        => _function.Invoke(number1, number2);
}
