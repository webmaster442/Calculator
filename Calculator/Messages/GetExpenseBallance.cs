//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal class GetExpenseBallance : PayloadBase
{
    public decimal Ballance { get; }

    public GetExpenseBallance(decimal ballance)
    {
        Ballance = ballance;
    }
}
