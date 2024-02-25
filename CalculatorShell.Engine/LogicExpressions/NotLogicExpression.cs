//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

namespace CalculatorShell.Engine.LogicExpressions;

internal sealed class NotLogicExpression : ILogicExpression
{

    public ILogicExpression Child { get; }

    public NotLogicExpression(ILogicExpression child)
    {
        Child = child;
    }

    public bool Evaluate(IDictionary<string, bool> variables)
        => !Child.Evaluate(variables);

    public string ToString(CultureInfo cultureInfo)
        => $"!({Child.ToString(cultureInfo)})";

    public override string ToString()
        => ToString(CultureInfo.InvariantCulture);
}
