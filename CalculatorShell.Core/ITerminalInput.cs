//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

public interface ITerminalInput
{
    object Prompt { get; set; }
    (string cmd, Arguments Arguments) ReadLine();
}
