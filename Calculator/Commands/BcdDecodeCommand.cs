using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

namespace Calculator.Commands;

internal class BcdDecodeCommand : ShellCommand
{
    public BcdDecodeCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["bcddecode"];

    public override string Synopsys => "Decode a number from binary coded decimal to decimal";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(1);

        string result = BcdConverter.BcdDecode(args[0]);

        Host.Output.Result(result);
    }
}