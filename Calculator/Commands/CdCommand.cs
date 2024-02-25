﻿//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.ArgumentCompleters;
using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class CdCommand : ShellCommandAsync
{
    public CdCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["cd"];

    public override string Synopsys
        => "Changes the current working directory";

    public override IArgumentCompleter? ArgumentCompleter
        => new DirectoryNameCompleter(Host);

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        string folder;
        if (args.Length < 1)
        {
            folder = await Host.Dialogs.SelectDirectory(cancellationToken);
        }
        else
        {
            folder = args[0];
        }
        Host.Mediator.Notify(new SetCurrentDir(folder));
    }
}
