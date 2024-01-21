using CalculatorShell.Core.Mediator;
using CalculatorShell.Engine;

namespace Calculator.Messages;
internal class AngleSystemMessage : PayloadBase
{
    public AngleSystemMessage(AngleSystem angleSystem)
    {
        AngleSystem = angleSystem;
    }

    public AngleSystem AngleSystem { get; }
}
