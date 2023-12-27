using CalculatorShell.Core;
using CalculatorShell.Core.Messenger;
using CalculatorShell.Engine;

namespace Calculator.Commands;

internal sealed class GradCommand : ShellCommand
{
    public GradCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["grad"];

    public override string Synopsys
        => "Changes the angle mode to gradians";

    public override void ExecuteInternal(Arguments args)
    {
        Host.MessageBus.Broadcast(new SimpleMessage<AngleSystem>(Guid.Empty, AngleSystem.Grad));
    }
}
