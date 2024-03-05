//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Internal;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal class VersionCommand : ShellCommand
{
    public VersionCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["version"];

    public override string Synopsys 
        => "prints out current program version";

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        Host.Output.Result(Helpers.GetAssemblyVersion().ToString());
    }
}
