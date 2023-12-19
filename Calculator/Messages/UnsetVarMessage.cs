using CalculatorShell.Core.Messenger;

namespace Calculator.Messages;

internal sealed class UnsetVarMessage : MessageBase
{
    public UnsetVarMessage(Guid sender, string variableName) : base(sender)
    {
        VariableName = variableName;
    }

    public string VariableName { get; }
}
