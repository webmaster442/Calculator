//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

using CommandLine;

namespace Calculator.Commands;

internal class BcdEncodeCommand : ShellCommand
{
    public BcdEncodeCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["bcdencode"];

    public override string Category
        => CommandCategories.Conversions;

    public override string Synopsys => "Encode a number to binary coded decimal";

    public override string HelpMessage
        => this.BuildHelpMessage<BcdEncodeOptions>();

    internal class BcdEncodeOptions
    {
        [Value(0, HelpText = "Number to encode", Required = true)]
        public Int128 Value { get; set; }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<BcdEncodeOptions>(Host);

        string result = BcdConverter.BcdEncode(options.Value);

        Host.Output.Result(result);
    }
}
