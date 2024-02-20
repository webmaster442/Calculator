namespace CalculatorShell.Core;

public interface ITerminalInput
{
    object Prompt { get; set; }
    (string cmd, Arguments Arguments) ReadLine();
}
