//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Represents a shell command that will be executed syncronously
/// </summary>
public interface ISyncShellCommand : IShellCommand
{
    /// <summary>
    /// The entry point of the command
    /// </summary>
    /// <param name="args">Command arguments</param>
    void Execute(Arguments args);
}
