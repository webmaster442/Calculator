using CalculatorShell.Core;

using Spectre.Console;

namespace Calculator;

internal sealed class MemoryLog : ILog
{
    private readonly Stack<string> _items;

    public MemoryLog()
    {
        _items = new Stack<string>();
    }

    public void Error(FormattableString text)
        => _items.Push($"{DateTime.Now} Error: {text}".EscapeMarkup());

    public void Exception(Exception ex)
        => _items.Push($"{DateTime.Now} Exception: {ex.Message}".EscapeMarkup());

    public void Info(FormattableString text)
        => _items.Push($"{DateTime.Now} Info: {text}".EscapeMarkup());

    public void Warning(FormattableString text)
        => _items.Push($"{DateTime.Now} Warning: {text}".EscapeMarkup());

    public void Clear()
        => _items.Clear();

    public IEnumerable<string> Entries
    {
        get
        {
            foreach (var item in _items)
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
}
