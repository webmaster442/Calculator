using Calculator.Messages;

using CalculatorShell.Core;
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

    public override void ExecuteInternal(Arguments args)
        => Host.Mediator.Notify(new AngleSystemMessage(AngleSystem.Deg));
}
