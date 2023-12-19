using System.Globalization;

namespace CalculatorShell.Engine.LogicExpressions;

internal sealed class ConstantLogicExpression : ILogicExpression
{
    public bool Value { get; }

    public ConstantLogicExpression(bool value)
    {
        Value = value;
    }

    public bool Evaluate(IDictionary<string, bool> variables)
        => Value;

    public string ToString(CultureInfo cultureInfo)
        => Value.ToString(cultureInfo);

    public override string ToString()
        => ToString(CultureInfo.InvariantCulture);
}
