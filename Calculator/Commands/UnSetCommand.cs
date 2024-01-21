using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class UnSetCommand : ShellCommand
{
    public UnSetCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["unset"];

    public override string Synopsys
        => "Unset a variable";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(1);
        var name = args.AsEnumerable().First();
        Host.Mediator.Notify(new UnsetVarMessage(name));
    }
}
