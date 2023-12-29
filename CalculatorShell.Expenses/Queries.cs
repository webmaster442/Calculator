namespace CalculatorShell.Expenses;

public static class ExpenseQuery
{
    public static decimal Ballance(IEnumerable<ExpenseItem> expenses)
    {
        decimal result = 0;

        foreach (ExpenseItem item in expenses)
        {
            if (item.IsIncome)
                result += item.Amount;
            else
                result -= item.Amount;
        }

        return result;
    }
}