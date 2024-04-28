//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Text;

using CalculatorShell.Core;
using CalculatorShell.Engine;

namespace Calculator.Tests.Calculator;

internal sealed class TestTerminalOutput : ITerminalOutput
{
    private readonly StringBuilder _buffer;

    public TestTerminalOutput()
    {
        _buffer = new StringBuilder();
    }

    public void BlankLine()
    {
        _ = _buffer.AppendLine();
    }

    public void BreakDown(IReadOnlyDictionary<string, double> items)
    {
        foreach (var item in items)
        {
            _ = _buffer.AppendFormat("{0} - {1}\r\n", item.Key, item.Value);
        }
    }

    public void Clear()
    {
        _ = _buffer.Clear();
    }

    public void Error(Exception ex)
    {
        _ = _buffer.AppendLine(ex.ToString());
    }

    public void Error(string message)
    {
        _ = _buffer.AppendLine(message);
    }

    public void List(string header, IEnumerable<string> data)
    {
        _ = _buffer.AppendLine(header);
        foreach (var item in data)
        {
            _ = _buffer.AppendLine(item);
        }
    }

    public void Write(string markup)
    {
        _ = _buffer.Append(markup);
    }

    public void MarkupLine(string markup)
    {
        _ = _buffer.AppendLine(markup);
    }

    public void Result(string message)
    {
        _ = _buffer.AppendLine(message);
    }

    public void Result(double value)
    {
        _ = _buffer.AppendLine(value.ToString(CultureInfo.InvariantCulture));
    }

    public void Result(Number value)
    {
        _ = _buffer.AppendLine(value.ToString(CultureInfo.InvariantCulture));
    }

    public void Result(decimal value)
    {
        _ = _buffer.AppendLine(value.ToString(CultureInfo.InvariantCulture));
    }

    public void Result(ICalculatorFormattable value)
    {
        _ = _buffer.AppendLine(value.ToString(CultureInfo.InvariantCulture, false));
    }

    public void Table(TableData tabledata)
    {
        foreach (var item in tabledata)
        {
            string row = string.Join(';', item);
            _ = _buffer.AppendLine(row);
        }
    }

    public override string ToString()
    {
        return _buffer.ToString();
    }

    public void Write(byte c)
    {
        _buffer.Append((char)c);
    }
}
