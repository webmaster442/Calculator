//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

namespace CalculatorShell.Engine.LogicExpressions;

internal sealed class VariableLogicExpression : ILogicExpression
{
    public string Name { get; }

    public VariableLogicExpression(string name)
    {
        Name = name;
    }

    public bool Evaluate(IDictionary<string, bool> variables)
        => variables[Name];

    public string ToString(CultureInfo cultureInfo)
        => Name.ToString(cultureInfo);

    public override string ToString()
        => ToString(CultureInfo.InvariantCulture);
}
