namespace CalculatorShell.Core;

public interface ITerminalInput
{
    (string cmd, Arguments Arguments) ReadLine();
}
