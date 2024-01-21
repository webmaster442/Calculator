using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal sealed class CurrentDirMessage : PayloadBase
{
    public CurrentDirMessage(string folder)
    {
        CurrentFolder = folder;
    }

    public string CurrentFolder { get; set; }
}
