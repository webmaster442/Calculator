using CalculatorShell.Core.Mediator;
using CalculatorShell.Engine;

namespace Calculator.Messages;
internal class AngleSystemChange : PayloadBase
{
    public AngleSystemChange(AngleSystem angleSystem)
    {
        AngleSystem = angleSystem;
    }

    public AngleSystem AngleSystem { get; }
}
