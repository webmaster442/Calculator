//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal class HelpRequestMessage : PayloadBase
{
    public string Command { get; }

    public HelpRequestMessage(string command)
    {
        Command = command;
    }
}
