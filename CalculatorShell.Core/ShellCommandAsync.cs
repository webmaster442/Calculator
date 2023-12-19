namespace CalculatorShell.Core;

public abstract class ShellCommandAsync : IShellCommand
{
    protected ShellCommandAsync(IHost host)
    {
        Host = host;
    }

    public IHost Host { get; }

    public abstract string[] Names { get; }

    public abstract Task Execute(Arguments args, CancellationToken cancellationToken);
}
