//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

public interface IShellCommand
{
    IHost Host { get; }
    string Synopsys { get; }
    string[] Names { get; }
    IArgumentCompleter? ArgumentCompleter { get; }
}
