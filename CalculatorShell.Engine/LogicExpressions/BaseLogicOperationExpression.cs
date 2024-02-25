//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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
