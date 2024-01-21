﻿using CalculatorShell.Core;

namespace Calculator.Commands;

internal class LogCommand : ShellCommand
{
    public LogCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["log"];

    public override string Synopsys =>
        "Display log entries";

    public override void ExecuteInternal(Arguments args)
    {
        foreach (var entry in Host.Log.Entries)
        {
            Host.Output.MarkupLine(entry);
        }
    }
}