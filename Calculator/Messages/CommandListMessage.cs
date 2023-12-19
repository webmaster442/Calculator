using CalculatorShell.Core.Messenger;

namespace Calculator.Messages;

internal class CommandListMessage : MessageBase
{
    public CommandListMessage(Guid sender) : base(sender)
    {
    }
}