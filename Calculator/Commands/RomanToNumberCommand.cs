//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

using CommandLine;

using Humanizer;

namespace Calculator.Commands;

internal sealed class RomanToNumberCommand : ShellCommand
{
    public RomanToNumberCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["roman-to-number"];

    public override string Category
        => CommandCategories.Conversions;

    public override string Synopsys
        => "Converts a roman number to an integer";

    public override string HelpMessage
        => this.BuildHelpMessage<RomanToNumberArguments>();

    internal class RomanToNumberArguments
    {
        [Value(0, Required = true, HelpText = "The roman number to convert to integer")]
        public string Roman { get; set; }

        public RomanToNumberArguments()
        {
            Roman = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<RomanToNumberArguments>(Host);
        try
        {
            Host.Output.Result(options.Roman.FromRoman().ToString());
        }
        catch (Exception ex)
        {
            throw new CommandException(ex.Message);
        }
    }
}
