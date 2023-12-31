﻿using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class ClearCommand : ShellCommand
{
    public ClearCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["cls", "clear"];

    public override string Synopsys
        => "Clears the terminal output";

    public override void ExecuteInternal(Arguments args)
    {
        Host.Output.Clear();
    }
}
