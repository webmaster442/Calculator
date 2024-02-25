//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal sealed class SetCurrentDir : PayloadBase
{
    public SetCurrentDir(string folder)
    {
        CurrentFolder = folder;
    }

    public string CurrentFolder { get; set; }
}
