
namespace CalculatorShell.Core;

public abstract class ShellCommand : ISyncShellCommand
{
    protected ShellCommand(IHost host)
    {
        Host = host;
    }

    public IHost Host { get; }

    public abstract string[] Names { get; }

    public abstract string Synopsys { get; }

    public virtual IArgumentCompleter? ArgumentCompleter => null;

    public abstract void ExecuteInternal(Arguments args);

    public void Execute(Arguments args)
    {
        try
        {
            ExecuteInternal(args);
        }
        catch (Exception ex)
        {
            Host.Log.Exception(ex);
            Host.Output.Error(ex);
        }
    }
}
