//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class ClearCommand : ShellCommand
{
    public ClearCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["cls", "clear"];

    public override string Synopsys
        => "Clears the terminal output";

    public override string Category
        => CommandCategories.Program;

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        Host.Output.Clear();
    }
}
