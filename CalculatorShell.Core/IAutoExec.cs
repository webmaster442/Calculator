//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

public interface IAutoExec
{
    string LogMessage { get; }
    int Priority { get; }
    void Execute(IHost host, IWritableHost writableHost);
}
