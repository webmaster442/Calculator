//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal class ManCommand : ShellCommand
{
    public ManCommand(IHost host) : base(host)
    {
    }

    public override string[] Names
        => ["man"];

    public override string Synopsys
        => "Opens the manual";

    public override void ExecuteInternal(Arguments args)
    {
        Host.Dialogs.OpenServerDocument(ServerDocument.Manual);
    }
}
