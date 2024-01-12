using CalculatorShell.Core.Messenger;

namespace Calculator.Messages;

internal sealed class RequestCurrentDirMessage : MessageBase
{
    public RequestCurrentDirMessage(Guid sender) : base(sender)
    {
    }
}