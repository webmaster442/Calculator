//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator.Commands;

internal class LogCommand : ShellCommand
{
    public LogCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["log"];

    public override string Synopsys =>
        "Display log entries";

    public override void ExecuteInternal(Arguments args)
    {
        foreach (var entry in Render(Host.Log.Entries))
        {
            Host.Output.MarkupLine(entry);
        }
    }

    private static IEnumerable<string> Render(IEnumerable<string> entries)
    {
        foreach (var item in entries)
        {
            if (item.Contains(" Warning: "))
                yield return $"[yellow]{item}[/]";
            else if (item.Contains(" Error: ") || item.Contains(" Exception: "))
                yield return $"[red]{item}[/]";
            else
                yield return item;
        }
    }
}
