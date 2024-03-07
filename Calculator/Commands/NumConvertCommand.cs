//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

using CommandLine;

namespace Calculator.Commands;

internal sealed class NumConvertCommand : ShellCommand
{
    private readonly NumberSystemConverter _converter;

    public NumConvertCommand(IHost host) : base(host)
    {
        _converter = new NumberSystemConverter();
    }

    public override string[] Names => ["num-convert"];

    public override string Category
        => CommandCategories.Conversions;

    public override string Synopsys
        => "Converts between number systems";

    public override string HelpMessage
        => this.BuildHelpMessage<NumConvertOptions>();

    internal class NumConvertOptions
    {
        [Value(0, HelpText = "Number to convert", Required = true)]
        public string Value { get; set; }

        [Value(1, HelpText = "Source number system", Required = true, Min = 2, Max = 36)]
        public int Source { get; set; }

        [Value(1, HelpText = "Target number system", Required = true, Min = 2, Max = 36)]
        public int Target { get; set; }

        public NumConvertOptions()
        {
            Value = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<NumConvertOptions>(Host);

        string result = _converter.Convert(options.Value, options.Source, options.Target);

        Host.Output.Result(result);
    }
}
