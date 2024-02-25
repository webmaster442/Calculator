//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal sealed class UnsetVariable : PayloadBase
{
    public UnsetVariable(string variableName)
    {
        VariableName = variableName;
    }

    public string VariableName { get; }
}
