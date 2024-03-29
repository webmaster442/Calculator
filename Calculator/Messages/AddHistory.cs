//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal sealed class AddHistory : PayloadBase
{
    public string CommandLine { get; }

    public AddHistory(string commandLine)
    {
        CommandLine = commandLine;
    }
}
