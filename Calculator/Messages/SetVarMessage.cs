using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal sealed class SetVarMessage : PayloadBase
{
    public SetVarMessage(string variableName, string expression)
    {
        VariableName = variableName;
        Expression = expression;
    }

    public string VariableName { get; }
    public string Expression { get; }
}
