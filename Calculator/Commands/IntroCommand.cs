using CalculatorShell.Core;

namespace Calculator.Commands;

internal class IntroCommand : IAutoExecShellCommand
{
    private static Version GetVersion()
    {
        return typeof(IntroCommand).Assembly.GetName().Version 
            ?? new Version(9999, 9999, 9999, 9999);
    }

    public void Execute(IHost host)
    {
        host.Output.MarkupLine($"[blue]Calculator {GetVersion()}[/]");
        host.Output.MarkupLine("To list commands type [lime]commands[/], to list supported functions type: [green]functions[/]");
        host.Output.MarkupLine("To exit type [yellow]exit[/] or [yellow]quit[/]");
        host.Output.BlankLine();
    }
}
