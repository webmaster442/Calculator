using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

namespace Calculator.Commands;
internal class FileSizeCommand : ShellCommand
{
    public FileSizeCommand(IHost host) : base(host)
    {
    }

    public override string[] Names
        => ["filesize"];

    public override string Synopsys
        => "Converts file sizes to human readable form";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(1);

        double value = args.Parse<double>(0);
        string unit = args.Length == 1 ? "bytes" : args[1];

        long bytes = FileSizeCalculator.ToBytes(value, unit);

        string result = FileSizeCalculator.ToHumanReadable(bytes);

        Host.Output.Result(result);
    }
}
