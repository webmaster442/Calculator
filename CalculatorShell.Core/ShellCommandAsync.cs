namespace CalculatorShell.Core;

public abstract class ShellCommandAsync : IAsyncShellCommand
{
    protected ShellCommandAsync(IHost host)
    {
        Host = host;
    }

    public IHost Host { get; }

    public abstract string[] Names { get; }

    public abstract string Synopsys { get; }

    public async Task Execute(Arguments args, CancellationToken cancellationToken)
    {
        try
        {
            await ExecuteInternal(args, cancellationToken);
        }
        catch (Exception ex)
        {
            Host.Output.Error(ex);
        }
    }

    public abstract Task ExecuteInternal(Arguments args, CancellationToken cancellationToken);
}
