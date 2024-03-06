using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal class AddHistory : PayloadBase
{
    public string CommandLine { get; }
    public bool Success { get; }

    public AddHistory(string commandLine, bool success)
    {
        CommandLine = commandLine;
        Success = success;
    }
}