//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Interface for structured logging
/// </summary>
public interface IStructuredLog
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
    /// <param name="context">an additonal object as contexct information</param>
    void Info(string text, object? context = null);
    /// <summary>
    /// Log a warning message
    /// </summary>
    /// <param name="text">message to log</param>
    /// <param name="context">an additonal object as contexct information</param>
    void Warning(string text, object? context = null);
    /// <summary>
    /// Log an error message
    /// </summary>
    /// <param name="text">message to log</param>
    /// /// <param name="context">an additonal object as contexct information</param>
    void Error(string text, object? context = null);
    /// <summary>
    /// Log entries
    /// </summary>
    IEnumerable<KeyValuePair<DateTime, string>> Entries { get; }
    /// <summary>
    /// Clears the log
    /// </summary>
    void Clear();
}
