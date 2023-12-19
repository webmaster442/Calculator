using CalculatorShell.Core.Messenger;

namespace Calculator.Messages;

internal class FunctionListMessage : MessageBase
{
    public FunctionListMessage(Guid sender) : base(sender)
    {
    }
}
