using CalculatorShell.Core.Mediator;
using CalculatorShell.Engine.Expenses;

namespace Calculator.Messages;

internal sealed class AddExpense : PayloadBase
{
    public AddExpense(ExpenseItem item)
    {
        Item = item;
    }

    public ExpenseItem Item { get; }
}
