using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class PwdCommand : ShellCommand
{
    public PwdCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["pwd"];

    public override string Synopsys
        => "Returns the current working directory";

    public override void ExecuteInternal(Arguments args)
    {
        string dir = Host.Mediator
            .Request<string, CurrentDirRequest>(new CurrentDirRequest())
            ?? throw new CommandException("Current folder is unknown");

        Host.Output.Result(dir);
    }
}
