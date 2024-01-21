using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Mediator;
using CalculatorShell.Engine.Expenses;

namespace Calculator.Internal;

internal sealed class Expenses :
    INotifyTarget<AddExpenseMessage>,
    IRequestProvider<ExpenseBallanceMessage, ExpenseBallanceRequestMessage>,
    IRequestProvider<TableData, ExpenseStatRequestMessage>,
    IRequestProvider<IReadOnlyDictionary<string, double>, ExpenseDistributionRequestMessage>
{
    private readonly ExpenseItemDb _db;
    private readonly string _folder;
    private readonly IHost _host;

    public Expenses(IHost host)
    {
        _host = host;
        _folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CalculatorShell");
        _db = new ExpenseItemDb(_folder);
        _host.Mediator.Register(this);

    }

    void INotifyTarget<AddExpenseMessage>.OnNotify(AddExpenseMessage message)
        => _db.Insert(message.Item);

    ExpenseBallanceMessage IRequestProvider<ExpenseBallanceMessage, ExpenseBallanceRequestMessage>.OnRequest(ExpenseBallanceRequestMessage message)
    {
        decimal result = 0;

        foreach (ExpenseItem item in _db.Items)
        {
            if (item.IsIncome)
                result += item.Amount;
            else
                result -= item.Amount;
        }

        return new ExpenseBallanceMessage(result);
    }

    TableData IRequestProvider<TableData, ExpenseStatRequestMessage>.OnRequest(ExpenseStatRequestMessage message)
    {
        List<ExpenseItem> spendings = _db.Items.Where(x => !x.IsIncome).ToList();

        int days = spendings.DistinctBy(x => x.Date).Count();

        decimal sumIncome = _db.Items.Where(x => x.IsIncome).Sum(x => x.Amount);
        decimal sumExpense = spendings.Sum(x => x.Amount);
        decimal ballance = sumIncome - sumExpense;

        TableData table = new("Description", "Value");
        table.AddRow("Income", sumIncome.ToString(_host.CultureInfo));
        table.AddRow("Expense", sumExpense.ToString(_host.CultureInfo));
        table.AddRow("Ballance", ballance.ToString(_host.CultureInfo));
        table.AddRow("Days with expenses", days.ToString(_host.CultureInfo));
        table.AddRow("Average spending / day", (sumExpense / days).ToString(_host.CultureInfo));

        return table;
    }

    IReadOnlyDictionary<string, double> IRequestProvider<IReadOnlyDictionary<string, double>, ExpenseDistributionRequestMessage>.OnRequest(ExpenseDistributionRequestMessage message)
    {
        return _db.Items
            .Where(x => !x.IsIncome)
            .GroupBy(x => x.Category.ToLowerInvariant())
            .ToDictionary(x => x.Key, x => Convert.ToDouble(x.Sum(x => x.Amount)));
    }
}
