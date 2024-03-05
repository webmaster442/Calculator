//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class PwdCommand : ShellCommand
{
    public PwdCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["pwd"];

    public override string Synopsys
        => "Returns the current working directory";

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        Host.Output.Result(Host.CurrentDirectory);
    }
}
