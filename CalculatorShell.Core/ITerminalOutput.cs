//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Engine;

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
    /// <param name="value">result value to display</param>
    void Result(double value);

    /// <summary>
    /// Display a result
    /// </summary>
    /// <param name="value">result value to display</param>
    void Result(Number value);

    /// <summary>
    /// Display a result
    /// </summary>
    /// <param name="value">result value to display</param>
    void Result(decimal value);

    /// <summary>
    /// Display a result
    /// </summary>
    /// <param name="value">result value to display</param>
    void Result(ICalculatorFormattable value);


    /// <summary>
    /// Display a result
    /// </summary>
    /// <param name="value">result value to display</param>
    void Result(string value);

    /// <summary>
    /// Display a markup line
    /// </summary>
    /// <param name="markup">Markup to display</param>
    void MarkupLine(string markup);
    /// <summary>
    /// Display a markup string
    /// </summary>
    /// <param name="markup">Markup to display</param>
    void Write(string markup);
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
    
    /// <summary>
    /// Display a single ASCII character
    /// </summary>
    /// <param name="c">ASCII character to display</param>
    void Write(byte c);
}
