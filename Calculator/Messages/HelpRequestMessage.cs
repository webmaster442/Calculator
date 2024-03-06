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
