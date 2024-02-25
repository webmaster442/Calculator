//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Concurrent;

using CalculatorShell.Core;

using Spectre.Console;

namespace Calculator;

internal sealed class MemoryLog : ILog
{
    private readonly ConcurrentDictionary<DateTime, string> _items;

    public MemoryLog()
    {
        _items = new ConcurrentDictionary<DateTime, string>();
    }

    public void Error(FormattableString text)
    {
        _items.TryAdd(DateTime.Now, $"Error: {text}");
    }

    public void Exception(Exception ex)
    {
        _items.TryAdd(DateTime.Now, $"Exception: {ex.Message}");
    }

    public void Info(FormattableString text)
    {
        _items.TryAdd(DateTime.Now, $"Info: {text}");
    }

    public void Warning(FormattableString text)
    {
        _items.TryAdd(DateTime.Now, $"Warning: {text}");
    }

    public void Clear()
        => _items.Clear();

    public IEnumerable<KeyValuePair<DateTime, string>> Entries
        => _items;
}
