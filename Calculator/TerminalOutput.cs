using System.Globalization;

using CalculatorShell.Core;
using CalculatorShell.Engine;

using Spectre.Console;

namespace Calculator;

internal class TerminalOutput : ITerminalOutput
{
    internal CultureInfo CultureInfo { get; set; }
    private readonly Color[] _palette;

    public TerminalOutput()
    {
        CultureInfo = CultureInfo.InvariantCulture;
        _palette = InitializePalette();
    }

    private Color[] InitializePalette()
    {
        return typeof(Color)
            .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Where(p => p.PropertyType == typeof(Color))
            .Select(p => (Color)p.GetValue(null)!)
            .ToArray();
    }

    public void Clear()
        => AnsiConsole.Clear();

    public void Error(Exception ex)
    {
        if (ex is ComandException or EngineException or CommandException)
        {
            Error(ex.Message);
            return;
        }

        AnsiConsole.WriteException(ex);
    }

    public void Error(string message)
        => AnsiConsole.MarkupLine($"[red]{message.EscapeMarkup()}[/]");

    public void Result(string message)
        => AnsiConsole.MarkupLine($"[green]{message.EscapeMarkup()}[/]");

    public void List(string header, IEnumerable<string> data)
    {
        AnsiConsole.MarkupLine($"[green]{header.EscapeMarkup()}[/]");
        var columns = new Columns(data);
        AnsiConsole.Write(columns);
    }

    public void BlankLine()
        => AnsiConsole.WriteLine();

    public void MarkupLine(string markup)
        => AnsiConsole.MarkupLine(markup);

    public void Table(TableData tabledata)
    {
        var table = new Table();
        table.AddColumns(tabledata.Headers);
        foreach (var row in tabledata)
        {
            table.AddRow(row);
        }
        AnsiConsole.Write(table);
    }

    public void BreakDown(IReadOnlyDictionary<string, double> items)
    {
        static int GetIndex(string str)
        {
            int index = 2;
            for (int i=0; i< str.Length; i++)
            {
                index += i * str[i];
            }
            return index;
        }

        var b = new BreakdownChart()
            .FullSize();

        foreach (var item in items)
        {
            var color = _palette[GetIndex(item.Key) % _palette.Length];
            b.AddItem(item.Key, item.Value, color);
        }
        AnsiConsole.Write(b);
    }

    public void Markup(string markup)
    {
        Console.Write(markup);
    }
}
