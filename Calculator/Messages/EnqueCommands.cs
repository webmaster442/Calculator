using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;
internal class EnqueCommands : PayloadBase
{
    public EnqueCommands(string[] commands)
    {
        Commands = commands;
    }

    public string[] Commands { get; }
}
