//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class FunctionsCommand : ShellCommand
{
    public FunctionsCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["functions"];


    public override string Category
        => CommandCategories.Program;

    public override string Synopsys
        => "Prints out the list of available functions in eval mode";

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        var data = Host.Mediator
            .Request<IEnumerable<string>, FunctionListRequest>(new FunctionListRequest())
            ?? Enumerable.Empty<string>();

        Host.Output.List("available functions:", data);
    }
}