using CalculatorShell.Core.Messenger;

namespace Calculator.Messages;

internal class FunctionListRequestMessage : MessageBase
{
    public FunctionListRequestMessage(Guid sender) : base(sender)
    {
    }
}
