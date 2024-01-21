using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal sealed class UnsetVarMessage : PayloadBase
{
    public UnsetVarMessage(string variableName)
    {
        VariableName = variableName;
    }

    public string VariableName { get; }
}
