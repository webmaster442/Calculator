//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class PlotCommand : ShellCommand
{
    public PlotCommand(IHost host) : base(host)
    {
    }

    public override string[] Names
        => ["plot"];

    public override string Synopsys
        => "Opens the Plot Ui";

    public override string Category
        => CommandCategories.Program;

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        Host.Dialogs.OpenServerDocument(ServerDocument.Plot);
    }
}