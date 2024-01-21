using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal class ExpenseStatCommand : ShellCommand
{
    public ExpenseStatCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["expense-stat"];

    public override string Synopsys =>
        "Provides detailed statistics about the monthly expenses and incomes";

    public override void ExecuteInternal(Arguments args)
    {
        var statTable = Host.Mediator.Request<TableData, ExpenseStatRequestMessage>(new ExpenseStatRequestMessage())
            ?? throw new CommandException("No statistics are available");
        Host.Output.Table(statTable);

        var distribution = Host.Mediator.Request<IReadOnlyDictionary<string, double>, ExpenseDistributionRequestMessage>(new ExpenseDistributionRequestMessage())
            ?? throw new CommandException("No distribution is available");
        Host.Output.BreakDown(distribution);
    }
}