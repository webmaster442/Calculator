//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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

    public abstract string HelpMessage { get; }

    public virtual IArgumentCompleter? ArgumentCompleter => null;

    public async Task Execute(Arguments args, CancellationToken cancellationToken)
    {
        try
        {
            await ExecuteInternal(args, cancellationToken);
        }
        catch (Exception ex)
        {
            Host.Log.Exception(ex);
            Host.Output.Error(ex);
        }
    }

    public abstract Task ExecuteInternal(Arguments args, CancellationToken cancellationToken);
}
