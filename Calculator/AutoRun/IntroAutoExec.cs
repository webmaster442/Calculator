﻿using Calculator.Internal;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class IntroAutoExec : IAutoExec
{
    public string LogMessage => "Printing intro";

    public int Priority => int.MaxValue; //last

    public void Execute(IHost host)
    {
        host.Output.MarkupLine($"[blue]Calculator {Helpers.GetAssemblyVersion()}[/]");
        host.Output.MarkupLine("To list commands type [lime]commands[/], to list supported functions type: [green]functions[/]");
        host.Output.MarkupLine("To exit type [yellow]exit[/] or [yellow]quit[/]");
        host.Output.BlankLine();
    }
}