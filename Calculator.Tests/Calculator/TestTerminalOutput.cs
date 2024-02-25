//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

using CalculatorShell.Core;

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
        _buffer.AppendLine();
    }

    public void BreakDown(IReadOnlyDictionary<string, double> items)
    {
        foreach (var item in items)
        {
            _buffer.AppendFormat("{0} - {1}\r\n", item.Key, item.Value);
        }
    }

    public void Clear()
    {
        _buffer.Clear();
    }

    public void Error(Exception ex)
    {
        _buffer.AppendLine(ex.ToString());
    }

    public void Error(string message)
    {
        _buffer.AppendLine(message);
    }

    public void List(string header, IEnumerable<string> data)
    {
        _buffer.AppendLine(header);
        foreach (var item in data)
        {
            _buffer.AppendLine(item);
        }
    }

    public void Markup(string markup)
    {
        _buffer.Append(markup);
    }

    public void MarkupLine(string markup)
    {
        _buffer.AppendLine(markup);
    }

    public void Result(string message)
    {
        _buffer.AppendLine(message);
    }

    public void Table(TableData tabledata)
    {
        foreach (var item in tabledata)
        {
            string row = string.Join(';', item);
            _buffer.AppendLine(row);
        }
    }

    public override string ToString()
    {
        return _buffer.ToString();
    }
}
