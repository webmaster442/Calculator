//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics;

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;

internal sealed class TimeAddCommand : TimeCommand
{
    internal enum AddSubtractUnit
    {
        Hour,
        Minute,
        Second,
        Day,
        Month,
        Year,
    }

    private DateTime AddSubtractDates(DateTime start, string value, AddSubtractUnit unit)
    {
        return unit switch
        {
            AddSubtractUnit.Second => start.AddSeconds(double.Parse(value, Host.CultureInfo)),
            AddSubtractUnit.Minute => start.AddMinutes(double.Parse(value, Host.CultureInfo)),
            AddSubtractUnit.Hour => start.AddHours(double.Parse(value, Host.CultureInfo)),
            AddSubtractUnit.Day => start.AddDays(double.Parse(value, Host.CultureInfo)),
            AddSubtractUnit.Month => start.AddMonths(int.Parse(value, Host.CultureInfo)),
            AddSubtractUnit.Year => start.AddYears(int.Parse(value, Host.CultureInfo)),
            _ => throw new UnreachableException(),
        };
    }

    public TimeAddCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["time-add"];

    public override string Synopsys
        => "Add or subtract from a given date/time value";

    internal class TimeAddOptions
    {
        [Value(0, HelpText = "Date and time for the basis of calculation", Required = true)]
        public DateTime Start { get; set; }

        [Value(1, HelpText = "Value to add", Required = true)]
        public string Value { get; set; }

        [Value(2, HelpText = "Value unit", Required = true)]
        public AddSubtractUnit Unit { get; set; }

        public TimeAddOptions()
        {
            Value = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<TimeAddOptions>(Host);

        DateTime result = AddSubtractDates(options.Start, options.Value, options.Unit);
        Host.Output.Result(FormatTime(result));
    }
}
