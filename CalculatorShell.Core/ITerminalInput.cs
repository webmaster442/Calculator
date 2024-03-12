//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Terminal input
/// </summary>
public interface ITerminalInput
{
    /// <summary>
    /// Current prompt
    /// </summary>
    object Prompt { get; set; }
    /// <summary>
    /// Reads command name and arguments
    /// </summary>
    /// <returns>Command name and arguments</returns>
    (string cmd, Arguments Arguments) ReadLine();
}
