using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class ClearCommand : ShellCommand
{
    public ClearCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["cls", "clear"];

    public override void Execute(Arguments args)
    {
        Host.Output.Clear();
    }
}
