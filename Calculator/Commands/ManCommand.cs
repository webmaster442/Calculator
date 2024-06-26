﻿//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class ManCommand : ShellCommand
{
    public ManCommand(IHost host) : base(host)
    {
    }

    public override string[] Names
        => ["man"];

    public override string Synopsys
        => "Opens the manual";

    public override string Category
        => CommandCategories.Program;

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        Host.Dialogs.OpenServerDocument(ServerDocument.Manual);
    }
}
