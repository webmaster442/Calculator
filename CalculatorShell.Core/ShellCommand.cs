//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Represents a base class for a sync shell command
/// </summary>
public abstract class ShellCommand : ISyncShellCommand
{
    /// <summary>
    /// Creates a new instance of ShellCommand
    /// </summary>
    /// <param name="host">Command host API</param>
    protected ShellCommand(IHost host)
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
    public virtual IArgumentCompleter? ArgumentCompleter => null;

    /// <inheritdoc/>
    public abstract string HelpMessage { get; }

    /// <summary>
    /// Entry point of the command that is executed in an exception handled environment
    /// </summary>
    /// <param name="args">Command arguments</param>
    public abstract void ExecuteInternal(Arguments args);

    /// <inheritdoc/>
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
