//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Log
/// </summary>
public interface ILog
{
    /// <summary>
    /// Log an exception
    /// </summary>
    /// <param name="ex">An Exception to log</param>
    void Exception(Exception ex);
    /// <summary>
    /// Log an infromation message
    /// </summary>
    /// <param name="text">message to log</param>
    void Info(FormattableString text);
    /// <summary>
    /// Log a warning message
    /// </summary>
    /// <param name="text">message to log</param>
    void Warning(FormattableString text);
    /// <summary>
    /// Log an error message
    /// </summary>
    /// <param name="text">message to log</param>
    void Error(FormattableString text);
    /// <summary>
    /// Log entries
    /// </summary>
    IEnumerable<KeyValuePair<DateTime, string>> Entries { get; }
    /// <summary>
    /// Clears the log
    /// </summary>
    void Clear();
}
