using CalculatorShell.Core;

namespace Calculator;

internal sealed class MemoryLog : ILog
{
    private readonly List<string> _items;

    public MemoryLog()
    {
        _items = new List<string>();
    }

    public void Error(FormattableString text)
        => _items.Add($"{DateTime.Now} Error: {text}");

    public void Exception(Exception ex) 
        => _items.Add($"{DateTime.Now} Exception: {ex.Message}");

    public void Info(FormattableString text)
        => _items.Add($"{DateTime.Now} Info: {text}");

    public void Warning(FormattableString text)
        => _items.Add($"{DateTime.Now} Warning: {text}");
}
