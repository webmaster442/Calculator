using CalculatorShell.Core.Messenger;

namespace Calculator.Messages;

internal sealed class ExpenseBallanceRequestMessage : MessageBase
{
    public ExpenseBallanceRequestMessage(Guid sender) : base(sender)
    {
    }
}
