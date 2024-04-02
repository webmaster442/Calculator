//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

using CommandLine;

using Humanizer;

namespace Calculator.Commands;

internal sealed class NumberToTextCommand : ShellCommand
{
    public NumberToTextCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["number-to-text"];

    public override string Category
        => CommandCategories.Conversions;

    public override string Synopsys
        => "Converts a number to a string";

    public override string HelpMessage
        => this.BuildHelpMessage<NumberToTextArguments>();

    internal sealed class NumberToTextArguments
    {
        [Value(0, Required = true, HelpText = "The number to convert to text")]
        public long Number { get; set; }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<NumberToTextArguments>(Host);
        try
        {
            Host.Output.Result(options.Number.ToWords());
        }
        catch (Exception ex)
        {
            throw new CommandException(ex.Message);
        }
    }
}
