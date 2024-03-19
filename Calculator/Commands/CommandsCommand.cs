//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class CommandsCommand : ShellCommand
{
    public CommandsCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["commands", "cmds"];

    public override string Category
        => CommandCategories.Program;

    public override string Synopsys
        => "Prints out the list of available commands";

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        var data = Host.Mediator
            .Request<IDictionary<string, HashSet<string>>, CommandList>(new CommandList())
            ?? throw new CommandException("There are no available commands");

        foreach (var group in data)
        {
            Host.Output.List(group.Key, group.Value.Order());
            Host.Output.BlankLine();
        }
    }
}