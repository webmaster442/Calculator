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

    public override void Execute(Arguments args)
    {
        Host.MessageBus.Broadcast(new SimpleMessage<AngleSystem>(Guid.Empty, AngleSystem.Grad));
    }
}
