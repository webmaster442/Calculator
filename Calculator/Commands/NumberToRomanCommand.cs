//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

using CommandLine;

using Humanizer;

namespace Calculator.Commands;

internal sealed class NumberToRomanCommand : ShellCommand
{
    public NumberToRomanCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["number-to-roman"];

    public override string Category
        => CommandCategories.Conversions;

    public override string Synopsys
        => "Converts an integer to a roman number";

    public override string HelpMessage 
        => this.BuildHelpMessage<NumberToRomanArguments>();

    internal class NumberToRomanArguments
    {
        [Value(0, Required = true, HelpText = "The number to convert to roman")]
        public int Number { get; set; }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<NumberToRomanArguments>(Host);
        try
        {
            Host.Output.Result(options.Number.ToRoman());
        }
        catch (Exception ex)
        {
            throw new CommandException(ex.Message);
        }
    }
}
