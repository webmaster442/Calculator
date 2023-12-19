using System.Globalization;

namespace CalculatorShell.Engine.LogicExpressions;

internal sealed class AndLogicExpression : BaseLogicOperationExpression
{
    public AndLogicExpression(ILogicExpression left, ILogicExpression right) : base(left, right)
    {
    }

    public override bool Evaluate(IDictionary<string, bool> variables)
        => Left.Evaluate(variables) && Right.Evaluate(variables);

    public override string ToString(CultureInfo cultureInfo)
        => $"({Left.ToString(cultureInfo)}) & ({Right.ToString(cultureInfo)})";
}
