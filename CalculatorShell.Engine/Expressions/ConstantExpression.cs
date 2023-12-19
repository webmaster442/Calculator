using System.Globalization;

namespace CalculatorShell.Engine.Expressions;

internal sealed class ConstantExpression : IExpression
{
    public ConstantExpression(Number value)
    {
        Value = value;
    }

    public Number Value { get; }

    public Number Evaluate() => Value;

    public IExpression Simplify()
        => new ConstantExpression(Value);

    public string ToString(CultureInfo cultureInfo)
        => Value.ToString(cultureInfo) ?? string.Empty;
}
