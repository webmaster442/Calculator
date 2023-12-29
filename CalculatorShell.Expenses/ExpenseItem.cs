﻿namespace CalculatorShell.Expenses;

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