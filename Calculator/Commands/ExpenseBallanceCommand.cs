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
        var ballance = Host.MessageBus.Request<decimal, ExpenseBallanceRequestMessage>(new ExpenseBallanceRequestMessage(Guid.Empty)).First();
        Host.Output.Result(ballance.ToString(Host.CultureInfo));
    }
}
