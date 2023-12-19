
namespace CalculatorShell.Core;

public abstract class ShellCommand : IShellCommand
{
    protected ShellCommand(IHost host)
    {
        Host = host;
    }

    public IHost Host { get; }

    public abstract string[] Names { get; }

    public abstract void Execute(Arguments args);

    public Task Execute(Arguments args, CancellationToken cancellationToken)
    {
        return Task.Run(() => SeafeExecute(args), cancellationToken);
    }

    private void SeafeExecute(Arguments args)
    {
        try
        {
            Execute(args);
        }
        catch (Exception ex)
        {
            Host.Output.Error(ex);
        }
    }
}
