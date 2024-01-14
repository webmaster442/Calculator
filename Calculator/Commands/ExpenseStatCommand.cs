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
        var statTable = Host.MessageBus.Request<TableData, ExpenseStatRequestMessage>(new ExpenseStatRequestMessage(Guid.Empty)).First();
        Host.Output.Table(statTable);

        var distribution = Host.MessageBus.Request<IReadOnlyDictionary<string, double>, ExpenseDistributionRequestMessage>(new ExpenseDistributionRequestMessage(Guid.Empty)).First();
        Host.Output.BreakDown(distribution);
    }
}