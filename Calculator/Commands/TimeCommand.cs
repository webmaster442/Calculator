//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics;

using CalculatorShell.Core;

namespace Calculator.Commands;
internal sealed class TimeCommand : ShellCommand
{
    public TimeCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["time"];

    public override string Synopsys
        => "time calculations";

    internal enum SubCommand
    {
        Add,
        Subtract,
        Diff,
        Next
    }

    internal enum AddSubtractUnit
    {
        Hour,
        Minute,
        Second,
        Day,
        Month,
        Year,
    }

    private DateTime Parse(string str)
    {
        if (str.Equals("now", StringComparison.InvariantCultureIgnoreCase))
        {
            return DateTime.Now;
        }
        return DateTime.Parse(str, Host.CultureInfo);
    }

    private DateTime AddSubtractDates(DateTime start, string value, AddSubtractUnit unit, int multiplyby)
    {
        return unit switch
        {
            AddSubtractUnit.Second => start.AddSeconds(multiplyby * double.Parse(value, Host.CultureInfo)),
            AddSubtractUnit.Minute => start.AddMinutes(multiplyby * double.Parse(value, Host.CultureInfo)),
            AddSubtractUnit.Hour => start.AddHours(multiplyby *double.Parse(value, Host.CultureInfo)),
            AddSubtractUnit.Day => start.AddDays(multiplyby * double.Parse(value, Host.CultureInfo)),
            AddSubtractUnit.Month => start.AddMonths(multiplyby * int.Parse(value, Host.CultureInfo)),
            AddSubtractUnit.Year => start.AddYears(multiplyby * int.Parse(value, Host.CultureInfo)),
            _ => throw new UnreachableException(),
        };
    }


    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(2);

        DateTime timeA = Parse(args[0]);
        var cmd = args.Parse<SubCommand>(1);

        switch (cmd)
        {
            case SubCommand.Add:
            case SubCommand.Subtract:
                {
                    args.ThrowIfNotSpecifiedAtLeast(4);
                    int multiplier = cmd == SubCommand.Add ? 1 : -1;
                    var result = AddSubtractDates(timeA, args[2], args.Parse<AddSubtractUnit>(3), multiplier);
                    Host.Output.Result(result.ToString(Host.CultureInfo));
                }
                break;
            case SubCommand.Diff:
                {
                    args.ThrowIfNotSpecifiedAtLeast(3);
                    DateTime timeB = Parse(args[2]);
                    var diffResult = timeA - timeB;
                    Host.Output.Result(diffResult.ToString());
                }
                break;
            case SubCommand.Next:
                {
                    var nextDay = args.Parse<DayOfWeek>(2);
                    DateTime result = timeA;
                    while (result.DayOfWeek != nextDay)
                    {
                        result = result.AddDays(1);
                    }
                    Host.Output.Result(result.ToString(Host.CultureInfo));
                }
                break;
            default:
                throw new UnreachableException();
        }
    }
}
