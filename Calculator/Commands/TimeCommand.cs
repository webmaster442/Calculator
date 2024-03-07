//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

using static Calculator.Commands.UnitConvertConmmand;

namespace Calculator.Commands;

internal class TimeCommand : ShellCommand
{
    public TimeCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["time"];

    public override string Synopsys
        => "Display current Date & time";

    public override string Category
        => CommandCategories.Time;

    public override string HelpMessage
        => this.BuildHelpMessage<UnitConvertOptions>();

    public override void ExecuteInternal(Arguments args)
    {
        DateTime current = Host.Now();
        Host.Output.Result(FormatTime(current));
    }

    protected DateTime Parse(string str)
    {
        if (str.Equals("now", StringComparison.InvariantCultureIgnoreCase))
        {
            return Host.Now();
        }
        return DateTime.Parse(str, Host.CultureInfo);
    }

    protected string FormatTime(DateTime dt)
    {
        return $"""
               {dt.ToString(Host.CultureInfo)}
               Day of week: {dt.DayOfWeek}
               Day of year: {dt.DayOfYear}
               """;
    }
}
