using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Engine;

namespace Calculator.Commands;
internal sealed class RadCommand : ShellCommand
{
    public RadCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["rad"];

    public override string Synopsys
        => "Changes the angle mode to radians";

    public override void ExecuteInternal(Arguments args)
        => Host.Mediator.Notify(new AngleSystemChange(AngleSystem.Rad));
}
