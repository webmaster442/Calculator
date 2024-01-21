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
