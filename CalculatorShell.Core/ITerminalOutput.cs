//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Terminal output
/// </summary>
public interface ITerminalOutput
{
    /// <summary>
    /// Insert a blank line
    /// </summary>
    void BlankLine();
    /// <summary>
    /// Clear terminal output
    /// </summary>
    void Clear();
    /// <summary>
    /// Display an exception
    /// </summary>
    /// <param name="ex">Exception to display</param>
    void Error(Exception ex);
    /// <summary>
    /// Display an error
    /// </summary>
    /// <param name="message">error message</param>
    void Error(string message);
    /// <summary>
    /// Display a list of data
    /// </summary>
    /// <param name="header">List header</param>
    /// <param name="data">List data</param>
    void List(string header, IEnumerable<string> data);
    /// <summary>
    /// Display a result
    /// </summary>
    /// <param name="message">result message</param>
    void Result(string message);
    /// <summary>
    /// Display a markup line
    /// </summary>
    /// <param name="markup">Markup to display</param>
    void MarkupLine(string markup);
    /// <summary>
    /// Display a markup string
    /// </summary>
    /// <param name="markup">Markup to display</param>
    void Markup(string markup);
    /// <summary>
    /// Display a table
    /// </summary>
    /// <param name="tabledata">Table data</param>
    void Table(TableData tabledata);
    /// <summary>
    /// Display a breakdwon chart
    /// </summary>
    /// <param name="items">breakdownchart items</param>
    void BreakDown(IReadOnlyDictionary<string, double> items);
}
