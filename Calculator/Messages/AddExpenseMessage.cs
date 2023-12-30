using CalculatorShell.Core.Messenger;
using CalculatorShell.Engine.Expenses;

namespace Calculator.Messages;

internal sealed class AddExpenseMessage : MessageBase
{
    public AddExpenseMessage(Guid sender, ExpenseItem item) : base(sender)
    {
        Item = item;
    }

    public ExpenseItem Item { get; }
}
