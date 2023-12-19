using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class FunctionsCommand : ShellCommand
{
    public FunctionsCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["functions"];

    public override void Execute(Arguments args)
    {
        var data = Host.MessageBus
            .Request<IEnumerable<string>, FunctionListMessage>(new FunctionListMessage(Guid.Empty))
            .FirstOrDefault() ?? Enumerable.Empty<string>();

        Host.Output.List("available functions:", data);
    }
}