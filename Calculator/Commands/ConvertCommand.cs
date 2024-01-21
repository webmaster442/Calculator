using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

namespace Calculator.Commands;

internal sealed class ConvertCommand : ShellCommand
{
    private readonly NumberSystemConverter _converter;

    public ConvertCommand(IHost host) : base(host)
    {
        _converter = new NumberSystemConverter();
    }

    public override string[] Names => ["convert"];

    public override string Synopsys
        => "Converts between number systems";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(3);

        string result = _converter.Convert(args[0], args.Parse<int>(1), args.Parse<int>(2));

        Host.Output.Result(result);
    }
}
