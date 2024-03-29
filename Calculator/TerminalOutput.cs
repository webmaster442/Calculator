//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

using Calculator.Configuration;

using CalculatorShell.Core;
using CalculatorShell.Engine;

using Spectre.Console;

namespace Calculator;

internal sealed class TerminalOutput : ITerminalOutput
{
    internal CultureInfo CultureInfo { get; set; }
    private readonly Color[] _palette;
    private readonly Func<Config> _configAccessor;

    public TerminalOutput(Func<Config> configAccessor)
    {
        CultureInfo = CultureInfo.InvariantCulture;
        _palette = InitializePalette();
        _configAccessor = configAccessor;
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
        if (ex is TaskCanceledException
            or EngineException
            or CommandException)
        {
            Error(ex.Message);
            return;
        }

        AnsiConsole.WriteException(ex);
    }

    public void Error(string message)
        => AnsiConsole.MarkupLine($"[red]{message.EscapeMarkup()}[/]");

    public void Result(double value)
        => AnsiConsole.MarkupLine($"[green]{NumberFomatter.ToString(value, CultureInfo, _configAccessor().ThousandGroupping)}[/]");

    public void Result(decimal value)
        => AnsiConsole.MarkupLine($"[green]{NumberFomatter.ToString(value, CultureInfo, _configAccessor().ThousandGroupping)}[/]");


    public void Result(Number value)
        => AnsiConsole.MarkupLine($"[green]{NumberFomatter.ToString(value, CultureInfo, _configAccessor().ThousandGroupping)}[/]");

    public void Result(ICalculatorFormattable value)
        => AnsiConsole.MarkupLine($"[green]{value.ToString(CultureInfo, _configAccessor().ThousandGroupping)}[/]");

    public void Result(string message)
        => AnsiConsole.MarkupLine($"[green]{message.EscapeMarkup()}[/]");

    public void List(string header, IEnumerable<string> data)
    {
        AnsiConsole.MarkupLine($"[green]{header.EscapeMarkup()}[/]");
        var str = string.Join(' ', data);
        AnsiConsole.WriteLine(str);
    }

    public void BlankLine()
        => AnsiConsole.WriteLine();

    public void MarkupLine(string markup)
        => AnsiConsole.MarkupLine(markup);

    public void Table(TableData tabledata)
    {
        var table = new Table();
        _ = table.AddColumns(tabledata.Headers);
        foreach (var row in tabledata)
        {
            _ = table.AddRow(row.Select(x => x.EscapeMarkup()).ToArray());
        }
        AnsiConsole.Write(table);
    }

    public void BreakDown(IReadOnlyDictionary<string, double> items)
    {
        static int GetIndex(string str)
        {
            int index = 2;
            for (int i = 0; i < str.Length; i++)
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
            _ = b.AddItem(item.Key, item.Value, color);
        }
        AnsiConsole.Write(b);
    }

    public void Markup(string markup)
    {
        Console.Write(markup);
    }
}
