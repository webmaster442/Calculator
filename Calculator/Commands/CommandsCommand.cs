﻿using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class CommandsCommand : ShellCommand
{
    public CommandsCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["commands", "cmds"];

    public override string Synopsys
        => "Prints out the list of available commands";

    public override void ExecuteInternal(Arguments args)
    {
        var data = Host.Mediator
            .Request<IEnumerable<string>, CommandListMessage>(new CommandListMessage())
            ?? throw new CommandException("There are no available commands");

        Host.Output.List("available commands:", data.Order());
    }
}
