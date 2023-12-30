using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Messenger;
using CalculatorShell.Engine.Expenses;

namespace Calculator;

internal sealed class Expenses :
    IMessageClient<AddExpenseMessage>,
    IMessageProvider<decimal, ExpenseBallanceRequestMessage>,
    IMessageProvider<TableData, ExpenseStatRequestMessage>,
    IMessageProvider<IReadOnlyDictionary<string, double>, ExpenseDistributionRequestMessage>
{
    private readonly ExpenseItemDb _db;
    private readonly string _folder;
    private readonly IHost _host;

    public Expenses(IHost host)
    {
        _host = host;
        _folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CalculatorShell");
        _db = new ExpenseItemDb(_folder);
        ClientId = new Guid("DA687986-5145-49D6-8AC2-693809C863F7");
        _host.MessageBus.RegisterComponent(this);

    }

    public Guid ClientId { get; }

    void IMessageClient<AddExpenseMessage>.ProcessMessage(AddExpenseMessage input) 
        => _db.Insert(input.Item);

    decimal IMessageProvider<decimal, ExpenseBallanceRequestMessage>.ProvideMessage(ExpenseBallanceRequestMessage request)
    {
        decimal result = 0;

        foreach (ExpenseItem item in _db.Items)
        {
            if (item.IsIncome)
                result += item.Amount;
            else
                result -= item.Amount;
        }

        return result;
    }

    TableData IMessageProvider<TableData, ExpenseStatRequestMessage>.ProvideMessage(ExpenseStatRequestMessage request)
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

    IReadOnlyDictionary<string, double> IMessageProvider<IReadOnlyDictionary<string, double>, ExpenseDistributionRequestMessage>.ProvideMessage(ExpenseDistributionRequestMessage request)
    {
        return _db.Items
            .Where(x => !x.IsIncome)
            .GroupBy(x => x.Category.ToLowerInvariant())
            .ToDictionary(x => x.Key, x => Convert.ToDouble(x.Sum(x => x.Amount)));
    }
}
