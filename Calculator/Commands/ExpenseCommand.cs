//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Engine.Expenses;

using CommandLine;

namespace Calculator.Commands;

internal class ExpenseCommand : ShellCommand
{
    public ExpenseCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["expense"];

    public override string Synopsys
        => "Add an expense to the expenses";

    internal class ExpenseOptons
    {
        [Value(0, HelpText = "Expense or income value")]
        public decimal Ammount { get; set; }

        [Option('n', "name", HelpText = "Expense or income name")]
        public string Name { get; set; }

        [Option('c', "category", HelpText = "Expense or income category")]
        public string Category { get; set; }

        [Option('d', "date", HelpText = "Date of spending or income")]
        public DateOnly Date { get; set; }

        public ExpenseOptons()
        {
            Category = "Not categorized";
            Name = "Item with no name";
            Date = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }
    }

    protected ExpenseItem Create(Arguments args, bool income)
    {
        var options = args.Parse<ExpenseOptons>(Host);

        return new ExpenseItem
        {
            Amount = options.Ammount,
            Name = options.Name,
            Category = options.Category,
            Date = options.Date,
            IsIncome = income
        };
    }

    public override void ExecuteInternal(Arguments args)
    {
        var item = Create(args, false);
        Host.Mediator.Notify(new AddExpense(item));
    }
}
