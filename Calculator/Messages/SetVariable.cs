//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal sealed class SetVariable : PayloadBase
{
    public SetVariable(string variableName, string expression)
    {
        VariableName = variableName;
        Expression = expression;
    }

    public string VariableName { get; }
    public string Expression { get; }
}
