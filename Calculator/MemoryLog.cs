//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

using CalculatorShell.Core;

namespace Calculator;

internal sealed class MemoryLog : IStructuredLog
{
    private readonly ConcurrentDictionary<DateTime, string> _items;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly TimeProvider _timeProvider;

    public MemoryLog(TimeProvider timeProvider)
    {
        _items = new ConcurrentDictionary<DateTime, string>();
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        _timeProvider = timeProvider;
    }

    private void LogCore(string level, string msg, object? context)
    {
        if (context == null)
        {
            _items.TryAdd(_timeProvider.GetUtcNow().DateTime, $"{level} {msg}");
            return;
        }

        var json = JsonSerializer.Serialize(context, _jsonSerializerOptions);
        _items.TryAdd(_timeProvider.GetLocalNow().DateTime, $"{level} {msg} {json}");
    }

    public void Clear()
        => _items.Clear();

    public void Exception(Exception ex)
    {
#if DEBUG
        LogCore("Exception", ex.Message, new {
            StackTrace = ex.StackTrace?.Split('\n').Select(x => x.Trim()),
            InnerException = ex.InnerException?.Message,
        });
#else
        LogCore("Exception", ex.Message);
#endif
    }

    public void Info(string text, object? context = null)
    {
        LogCore("Info", text, context);
    }

    public void Warning(string text, object? context = null)
    {
        LogCore("Warning", text, context);
    }

    public void Error(string text, object? context = null)
    {
        LogCore("Error", text, context);
    }

    public IEnumerable<KeyValuePair<DateTime, string>> Entries
        => _items;
}
