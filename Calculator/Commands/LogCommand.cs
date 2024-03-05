//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator.Commands;

internal class LogCommand : ShellCommand
{
    public LogCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["log"];

    public override string Synopsys =>
        "Display log entries";

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        Host.Dialogs.OpenServerDocument(ServerDocument.Log);
    }
}
