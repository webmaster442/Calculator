//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Internal;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class IntroAutoExec : IAutoExec
{
    public string LogMessage => "Printing intro";

    public int Priority => int.MaxValue - 1; //2nd last

    public void Execute(IHost host)
    {
        host.Output.MarkupLine($"[blue]Calculator {Helpers.GetAssemblyVersion()}[/] | [aqua italic]https://github.com/webmaster442/Calculator[/]");
        host.Output.MarkupLine("To list commands type [lime]commands[/], to list supported functions type: [green]functions[/]");
        host.Output.MarkupLine("To exit type [yellow]exit[/] or [yellow]quit[/]");
        host.Output.BlankLine();
    }
}
