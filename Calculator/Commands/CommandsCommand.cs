//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Messages;

using CalculatorShell.Core;

using CommandLine;

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
            .Request<IEnumerable<IGrouping<string, (string, string[])>>, CommandList>(new CommandList())
            ?? throw new CommandException("There are no available commands");

        foreach (var group in data)
        {
            var items = group.SelectMany(g => g.Item2);
            Host.Output.List(group.Key, items.Order());
            Host.Output.BlankLine();
        }
    }
}
