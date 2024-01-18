using CalculatorShell.Core;
using CalculatorShell.Core.Messenger;

namespace Calculator.Commands;

internal sealed class ExecCommand : ShellCommand
{
    public ExecCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["exec"];

    public override string Synopsys
        => "Executes a file containing calculator commands";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(1);

        FileInfo fi = new(args[0]);

        if (!fi.Exists)
            throw new CommandException($"File doesn't exist: {args[0]}");

        if (fi.Length > 32 * 1024)
            throw new CommandException("File contains to many instructions > 32k");

        var instructions = File.ReadAllLines(args[0])
            .Where(x => !string.IsNullOrWhiteSpace(x) && !x.StartsWith('#'))
            .ToArray();

        Host.MessageBus.Broadcast(new SimpleMessage<string[]>(Guid.Empty, instructions));
    }
}
