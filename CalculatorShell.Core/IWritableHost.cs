//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

namespace CalculatorShell.Core;

/// <summary>
/// Writable host API
/// </summary>
public interface IWritableHost
{
    /// <summary>
    /// Setup commands for input
    /// </summary>
    /// <param name="commandHelps">command names and descriptions</param>
    /// <param name="completers">commands with completers</param>
    /// <param name="exitCommands">exit commands</param>
    void SetCommandData(IReadOnlyDictionary<string, string> commandHelps,
                        IReadOnlyDictionary<string, IArgumentCompleter> completers,
                        ISet<string> exitCommands);

    /// <summary>
    /// Current Directory
    /// </summary>
    string CurrentDirectory { get; set; }

    /// <summary>
    /// Culture
    /// </summary>
    CultureInfo CultureInfo { get; set; }
}
