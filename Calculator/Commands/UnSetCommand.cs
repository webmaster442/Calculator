using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class UnSetCommand : ShellCommand
{
    public UnSetCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["unset"];

    public override void Execute(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(1);
        var name = args.AsEnumerable().First();
        Host.MessageBus.Broadcast(new UnsetVarMessage(Guid.Empty, name));
    }
}
