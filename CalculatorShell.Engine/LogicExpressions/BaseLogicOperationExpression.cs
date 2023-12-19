using System.Globalization;

namespace CalculatorShell.Engine.LogicExpressions;

internal abstract class BaseLogicOperationExpression : ILogicExpression
{
    public BaseLogicOperationExpression(ILogicExpression left, ILogicExpression right)
    {
        Left = left;
        Right = right;
    }

    public ILogicExpression Left { get; }
    public ILogicExpression Right { get; }

    public abstract bool Evaluate(IDictionary<string, bool> variables);

    public abstract string ToString(CultureInfo cultureInfo);

    public override string ToString()
        => ToString(CultureInfo.InvariantCulture);
}
