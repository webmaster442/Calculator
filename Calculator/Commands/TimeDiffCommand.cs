//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class TimeDiffCommand : TimeCommand
{
    public TimeDiffCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["time-diff"];

    public override string Synopsys
        => "Calculates the difference between two Date/time values";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(2);

        DateTime timeA = Parse(args[0]);
        DateTime timeB = Parse(args[1]);

        TimeSpan result = timeA - timeB;

        Host.Output.Result(result.ToString());
    }
}
