//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;

internal sealed class TimeDiffCommand : TimeCommand
{
    public TimeDiffCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["time-diff"];

    public override string Synopsys
        => "Calculates the difference between two Date/time values";

    internal class TimeDiffOptions
    {
        [Value(0, HelpText = "First date and time", Required = true)]
        public DateTime A { get; set; }

        [Value(1, HelpText = "Second date and time", Required = true)]
        public DateTime B { get; set; }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<TimeDiffOptions>(Host);

        TimeSpan result = options.A - options.B;

        Host.Output.Result(result.ToString());
    }
}
