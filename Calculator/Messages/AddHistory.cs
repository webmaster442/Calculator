using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal class AddHistory : PayloadBase
{
    public string CommandLine { get; }

    public AddHistory(string commandLine)
    {
        CommandLine = commandLine;
    }
}
