using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal class ExpenseBallanceCommand : ShellCommand
{
    public ExpenseBallanceCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["expense-ballance"];

    public override string Synopsys =>
        "Gets the current month's ballance";

    public override void ExecuteInternal(Arguments args)
    {
        var response = Host.Mediator.Request<ExpenseBallanceMessage, ExpenseBallanceRequestMessage>(new ExpenseBallanceRequestMessage())
            ?? throw new CommandException("Ballance couldn't be deremined");
        Host.Output.Result(response.Ballance.ToString(Host.CultureInfo));
    }
}
