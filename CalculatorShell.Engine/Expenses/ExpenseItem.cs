//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Engine.Expenses;

public sealed class ExpenseItem
{
    public string Category { get; init; }
    public string Name { get; init; }
    public decimal Amount { get; init; }
    public DateOnly Date { get; init; }
    public bool IsIncome { get; init; }

    public ExpenseItem()
    {
        Category = string.Empty;
        Name = string.Empty;
        Amount = 0;
        Date = new DateOnly();
    }
}