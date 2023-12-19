using CalculatorShell.Core.Messenger;

namespace Calculator.Messages;

internal sealed class SetVarMessage : MessageBase
{
    public SetVarMessage(Guid sender, string variableName, string expression) : base(sender)
    {
        VariableName = variableName;
        Expression = expression;
    }

    public string VariableName { get; }
    public string Expression { get; }
}
