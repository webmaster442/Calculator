//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics;

using CalculatorShell.Core;

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

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(3);
        DateTime start = Parse(args[0]);
        AddSubtractUnit unit = args.Parse<AddSubtractUnit>(2);
        DateTime result = AddSubtractDates(start, args[1], unit);
        Host.Output.Result(FormatTime(result));
    }
}
