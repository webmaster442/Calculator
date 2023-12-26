using CalculatorShell.Core;
using CalculatorShell.Core.Messenger;
using CalculatorShell.Engine;

namespace Calculator.Commands;

internal sealed class DegCommand : ShellCommand
{
    public DegCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["deg"];

    public override string Synopsys
        => "Changes the angle mode to degrees";

    public override void Execute(Arguments args)
    {
        Host.MessageBus.Broadcast(new SimpleMessage<AngleSystem>(Guid.Empty, AngleSystem.Deg));
    }
}
