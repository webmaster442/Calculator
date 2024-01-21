using CalculatorShell.Core.Mediator;
using CalculatorShell.Engine.Expenses;

namespace Calculator.Messages;

internal sealed class AddExpenseMessage : PayloadBase
{
    public AddExpenseMessage(ExpenseItem item)
    {
        Item = item;
    }

    public ExpenseItem Item { get; }
}
