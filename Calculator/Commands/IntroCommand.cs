using CalculatorShell.Core;

namespace Calculator.Commands;

internal class IntroCommand : IAutoExecShellCommand
{
    public void Execute(IHost host)
    {
        host.Output.Result("TODO: Implement autorun");
    }
}
