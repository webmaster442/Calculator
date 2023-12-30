using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Engine.Expenses;

namespace Calculator.Commands;

internal class ExpenseCommand : ShellCommand
{
    public ExpenseCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["expense"];

    public override string Synopsys
        => "Add an expense to the expenses";

    protected static ExpenseItem Create(Arguments args, bool income)
    {
        string type = income ? "income" : "expense";

        args.ThrowIfNotSpecifiedAtLeast(1);

        return new ExpenseItem
        {
            Amount = args.Parse<decimal>(0),
            Name = args["-n", "--name"] ?? $"Unknown {type}",
            Category = args["-c", "--category"] ?? "Not categorized",
            Date = args.TryParse("-d", "--date", out DateOnly date) ? date : new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
            IsIncome = income
        };

    }

    public override void ExecuteInternal(Arguments args)
    {
        var item = Create(args, false);
        Host.MessageBus.Broadcast(new AddExpenseMessage(Guid.Empty, item));
    }
}
