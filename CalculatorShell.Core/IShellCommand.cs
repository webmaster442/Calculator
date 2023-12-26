﻿namespace CalculatorShell.Core;

public interface IShellCommand
{
    IHost Host { get; }
    string Synopsys { get; }
    string[] Names { get; }
    Task Execute(Arguments args, CancellationToken cancellationToken);
}
