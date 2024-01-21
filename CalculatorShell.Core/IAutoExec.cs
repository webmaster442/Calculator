namespace CalculatorShell.Core;

public interface IAutoExec
{
    string LogMessage { get; }
    int Priority { get; }
    void Execute(IHost host);
}
