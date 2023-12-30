using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class FunctionsCommand : ShellCommand
{
    public FunctionsCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["functions"];

    public override string Synopsys
        => "Prints out the list of available functions in eval mode";

    public override void ExecuteInternal(Arguments args)
    {
        var data = Host.MessageBus
            .Request<IEnumerable<string>, FunctionListRequestMessage>(new FunctionListRequestMessage(Guid.Empty))
            .FirstOrDefault() ?? Enumerable.Empty<string>();

        Host.Output.List("available functions:", data);
    }
}