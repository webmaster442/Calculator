//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Internal;

using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal class SetOptions : PayloadBase
{
    public SetOptions(Options options)
    {
        Options = options;
    }

    public Options Options { get; }
}