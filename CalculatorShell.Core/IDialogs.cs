namespace CalculatorShell.Core;

public interface IDialogs
{
    Task<string> SelectFile(CancellationToken cancellationToken);
    Task<string> SelectDirectory(CancellationToken cancellationToken);
}