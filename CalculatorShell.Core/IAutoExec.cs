//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Represents a command that will be executed on program startup
/// </summary>
public interface IAutoExec
{
    /// <summary>
    /// Message to add to the log
    /// </summary>
    string LogMessage { get; }
    /// <summary>
    /// Command priority
    /// </summary>
    int Priority { get; }
    /// <summary>
    /// Entry point
    /// </summary>
    /// <param name="host">Host interface API</param>
    /// <param name="writableHost">Writable Host API</param>
    void Execute(IHost host, IWritableHost writableHost);
}
