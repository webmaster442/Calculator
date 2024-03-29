//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core.Mediator;
using CalculatorShell.Engine;

namespace Calculator.Messages;
internal sealed class AngleSystemChange : PayloadBase
{
    public AngleSystemChange(AngleSystem angleSystem)
    {
        AngleSystem = angleSystem;
    }

    public AngleSystem AngleSystem { get; }
}
