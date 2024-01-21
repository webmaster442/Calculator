using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal class ExpenseBallanceMessage : PayloadBase
{
    public decimal Ballance { get; }

    public ExpenseBallanceMessage(decimal ballance)
    {
        Ballance = ballance;
    }
}
