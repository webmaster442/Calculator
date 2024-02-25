//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

public interface ITerminalOutput
{
    void BlankLine();
    void Clear();
    void Error(Exception ex);
    void Error(string message);
    void List(string header, IEnumerable<string> data);
    void Result(string message);
    void MarkupLine(string markup);
    void Markup(string markup);
    void Table(TableData tabledata);
    void BreakDown(IReadOnlyDictionary<string, double> items);
}
