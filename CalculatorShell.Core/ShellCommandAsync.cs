//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Represents a base class for an async shell command
/// </summary>
public abstract class ShellCommandAsync : IAsyncShellCommand
{
    /// <summary>
    /// Creates a new instance of ShellCommandAsync
    /// </summary>
    /// <param name="host">Command host API</param>
    protected ShellCommandAsync(IHost host)
    {
        Host = host;
    }

    /// <inheritdoc/>
    public IHost Host { get; }

    /// <inheritdoc/>
    public abstract string[] Names { get; }

    /// <inheritdoc/>
    public abstract string Category { get; }

    /// <inheritdoc/>
    public abstract string Synopsys { get; }

    /// <inheritdoc/>
    public abstract string HelpMessage { get; }

    /// <inheritdoc/>
    public virtual IArgumentCompleter? ArgumentCompleter => null;

    /// <inheritdoc/>
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

    /// <summary>
    /// Entry point of the command that is executed in an exception handled environment
    /// </summary>
    /// <param name="args">Command arguments</param>
    /// <param name="cancellationToken">A CancellationToken</param>
    /// <returns>A task</returns>
    public abstract Task ExecuteInternal(Arguments args, CancellationToken cancellationToken);
}
