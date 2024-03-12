//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Represents a shell command
/// </summary>
public interface IShellCommand
{
    /// <summary>
    /// Allows access to the command host API
    /// </summary>
    IHost Host { get; }

    /// <summary>
    /// Command description
    /// </summary>
    string Synopsys { get; }

    /// <summary>
    /// Command name with aliases
    /// </summary>
    string[] Names { get; }

    /// <summary>
    /// Command category
    /// </summary>
    string Category { get; }

    /// <summary>
    /// Argument completer assigned to the command
    /// </summary>
    IArgumentCompleter? ArgumentCompleter { get; }

    /// <summary>
    /// Command help message
    /// </summary>
    string HelpMessage { get; }
}
