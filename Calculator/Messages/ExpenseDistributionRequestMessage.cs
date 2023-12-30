using CalculatorShell.Core.Messenger;

namespace Calculator.Messages;

internal sealed class ExpenseDistributionRequestMessage : MessageBase
{
    public ExpenseDistributionRequestMessage(Guid sender) : base(sender)
    {
    }
}
