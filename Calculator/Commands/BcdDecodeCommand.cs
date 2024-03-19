//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

using CommandLine;

namespace Calculator.Commands;

internal class BcdDecodeCommand : ShellCommand
{
    public BcdDecodeCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["bcddecode"];

    public override string Category
        => CommandCategories.Conversions;

    public override string Synopsys
        => "Decode a number from binary coded decimal to decimal";

    public override string HelpMessage
        => this.BuildHelpMessage<BcdDecodeOptions>();

    internal class BcdDecodeOptions
    {
        [Value(0, HelpText = "Number to decode", Required = true)]
        public string Value { get; set; }

        public BcdDecodeOptions()
        {
            Value = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<BcdDecodeOptions>(Host);

        string result = BcdConverter.BcdDecode(options.Value);

        Host.Output.Result(result);
    }
}