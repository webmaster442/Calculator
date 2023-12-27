namespace CalculatorShell.Core;

public interface IAsyncShellCommand : IShellCommand
{
    Task Execute(Arguments args, CancellationToken cancellationToken);
}
