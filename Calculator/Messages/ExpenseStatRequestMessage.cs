using CalculatorShell.Core.Messenger;

namespace Calculator.Messages;

internal sealed class ExpenseStatRequestMessage : MessageBase
{
    public ExpenseStatRequestMessage(Guid sender) : base(sender)
    {
    }
}
