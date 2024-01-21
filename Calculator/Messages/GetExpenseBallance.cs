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
