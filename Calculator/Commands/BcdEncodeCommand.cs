using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

namespace Calculator.Commands;

internal class BcdEncodeCommand : ShellCommand
{
    public BcdEncodeCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["bcdencode"];

    public override string Synopsys => "Encode a number to binary coded decimal";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(1);

        string result = BcdConverter.BcdEncode(args.Parse<Int128>(0));

        Host.Output.Result(result);
    }
}
