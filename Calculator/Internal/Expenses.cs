using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Mediator;
using CalculatorShell.Engine.Expenses;

namespace Calculator.Internal;

internal sealed class Expenses :
    INotifyTarget<AddExpense>,
    IRequestProvider<GetExpenseBallance, ExpenseBallanceRequest>,
    IRequestProvider<TableData, ExpenseStatRequest>,
    IRequestProvider<IReadOnlyDictionary<string, double>, ExpenseDistributionRequest>
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

    void INotifyTarget<AddExpense>.OnNotify(AddExpense message)
        => _db.Insert(message.Item);

    GetExpenseBallance IRequestProvider<GetExpenseBallance, ExpenseBallanceRequest>.OnRequest(ExpenseBallanceRequest message)
    {
        decimal result = 0;

        foreach (ExpenseItem item in _db.Items)
        {
            if (item.IsIncome)
                result += item.Amount;
            else
                result -= item.Amount;
        }

        return new GetExpenseBallance(result);
    }

    TableData IRequestProvider<TableData, ExpenseStatRequest>.OnRequest(ExpenseStatRequest message)
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

    IReadOnlyDictionary<string, double> IRequestProvider<IReadOnlyDictionary<string, double>, ExpenseDistributionRequest>.OnRequest(ExpenseDistributionRequest message)
    {
        return _db.Items
            .Where(x => !x.IsIncome)
            .GroupBy(x => x.Category.ToLowerInvariant())
            .ToDictionary(x => x.Key, x => Convert.ToDouble(x.Sum(x => x.Amount)));
    }
}
