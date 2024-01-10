namespace CalculatorShell.Core;

public interface IDialogs
{
    Task<string> SelectFile(CancellationToken cancellationToken);
}