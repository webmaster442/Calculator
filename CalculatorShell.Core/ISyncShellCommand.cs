namespace CalculatorShell.Core;

public interface ISyncShellCommand : IShellCommand
{
    void Execute(Arguments args);
}
