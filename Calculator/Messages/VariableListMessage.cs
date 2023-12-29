using CalculatorShell.Core.Messenger;

namespace Calculator.Messages;

internal class VariableListMessage : MessageBase
{
    public VariableListMessage(Guid sender) : base(sender)
    {
    }
}
