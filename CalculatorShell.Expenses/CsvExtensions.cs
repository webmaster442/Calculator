namespace CalculatorShell.Expenses;

internal static class CsvExtensions
{
    public static string ToCsv(this ExpenseItem expenseItem)
    {
        return $"{expenseItem.Name};{expenseItem.Date};{expenseItem.Category};{expenseItem.Amount};{expenseItem.IsIncome}";
    }

    public static string ToCsvHeader(this ExpenseItem expenseItem)
    {
        return $"[{nameof(expenseItem.Name)}];[{nameof(expenseItem.Date)}];[{nameof(expenseItem.Category)}];[{nameof(expenseItem.Amount)}];[{nameof(expenseItem.IsIncome)}]";
    }

    public static ExpenseItem? ToExpenseItem(this string line)
    {
        var parts = line.Split(';', StringSplitOptions.RemoveEmptyEntries);
        try
        {
            return new ExpenseItem
            {
                Name = parts[0],
                Date = DateOnly.Parse(parts[1]),
                Category = parts[2],
                Amount = decimal.Parse(parts[3]),
                IsIncome = bool.Parse(parts[4])
            };
        }
        catch (Exception)
        {
            return null;
        }
    }
}
