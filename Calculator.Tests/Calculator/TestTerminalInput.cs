//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator.Tests.Calculator;

internal sealed class TestTerminalInput : ITerminalInput
{
    public string InputText { get; set; }

    public object Prompt { get; set; }

    public (string cmd, Arguments Arguments) ReadLine()
    {
        return ArgumentsFactory.Create(InputText);
    }

    public TestTerminalInput()
    {
        InputText = string.Empty;
        Prompt = string.Empty;
    }
}
