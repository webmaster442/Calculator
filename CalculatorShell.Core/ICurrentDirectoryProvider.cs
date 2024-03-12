//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Current directory provider
/// </summary>
public interface ICurrentDirectoryProvider
{
    /// <summary>
    /// Current directory
    /// </summary>
    string CurrentDirectory { get; set; }
}
