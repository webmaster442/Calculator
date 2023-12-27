using Calculator.Messages;

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
        var data = Host.MessageBus
            .Request<IEnumerable<string>, CommandListMessage>(new CommandListMessage(Guid.Empty))
            .FirstOrDefault() ?? Enumerable.Empty<string>();

        Host.Output.List("available commands:", data);
    }
}
