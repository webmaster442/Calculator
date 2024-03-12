//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Data;

namespace CalculatorShell.Core;

/// <summary>
/// Class responsible for loading commands
/// </summary>
public sealed class CommandLoader : IDisposable
{
    private readonly Dictionary<string, IShellCommand> _commands;
    private readonly Dictionary<string, string> _commandHelps;
    private readonly Dictionary<string, IArgumentCompleter> _completable;

    /// <summary>
    /// Loaded commands
    /// </summary>
    public IReadOnlyDictionary<string, IShellCommand> Commands
        => _commands;

    /// <summary>
    /// Loaded commands and their descriptions
    /// </summary>
    public IReadOnlyDictionary<string, string> CommandSynopsyses
        => _commandHelps;

    /// <summary>
    /// Commands that have argument completers
    /// </summary>
    public IReadOnlyDictionary<string, IArgumentCompleter> CompletableCommands
        => _completable;


    /// <summary>
    /// Creates a new instance of command loader
    /// </summary>
    /// <param name="atypeFromAssembly">A type that's assembly will be used to search for commands</param>
    /// <param name="host">Command host API</param>
    public CommandLoader(Type atypeFromAssembly, IHost host)
    {
        _commands = new Dictionary<string, IShellCommand>();
        _commandHelps = new Dictionary<string, string>();
        _completable = new Dictionary<string, IArgumentCompleter>();
        LoadCommands(atypeFromAssembly, host);
    }

    /// <summary>
    /// Loads additional types from an assembly
    /// </summary>
    /// <typeparam name="TType">Type to load</typeparam>
    /// <param name="atypeFromAssembly">A type that's assembly will be used to search for creatable <typeparamref name="TType"/></param>
    /// <param name="host">Command host API</param>
    /// <returns>Loaded types in an array</returns>
    public static IReadOnlyList<TType> LoadAdditionalTypes<TType>(Type atypeFromAssembly, IHost host)
        where TType : class
    {
        var types = GetTypesFromAssembly<TType>(atypeFromAssembly);
        List<TType> result = new();
        foreach (var type in types)
        {
            try
            {
                if (type.GetConstructors().First().GetParameters().Length > 0
                    && Activator.CreateInstance(type, host) is TType instanceWithParameter)
                {
                    result.Add(instanceWithParameter);
                }
                else if (Activator.CreateInstance(type) is TType instance)
                {
                    result.Add(instance);
                }
            }
            catch (Exception ex)
            {
                host.Output.Error(ex);
            }
        }
        return result;
    }

    private void LoadCommands(Type atypeFromAssembly, IHost host)
    {
        IEnumerable<Type> commandTypes = GetTypesFromAssembly<IShellCommand>(atypeFromAssembly);

        foreach (var commandType in commandTypes)
        {
            try
            {
                if (Activator.CreateInstance(commandType, new object[] { host }) is IShellCommand cmd)
                {
                    foreach (var name in cmd.Names)
                    {
                        _commands.Add(name, cmd);
                        _commandHelps.Add(name, cmd.Synopsys);

                        if (cmd.ArgumentCompleter != null)
                            _completable.Add(name, cmd.ArgumentCompleter);
                    }
                }
            }
            catch (Exception ex)
            {
                host.Output.Error(ex);
            }
        }
    }

    private static IEnumerable<Type> GetTypesFromAssembly<TInterface>(Type atypeFromAssembly)
    {
        return atypeFromAssembly.Assembly
            .GetTypes()
            .Where(t => !t.IsAbstract
                    && !t.IsInterface
                    && t.IsAssignableTo(typeof(TInterface)));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        foreach (var cmd in _commands)
        {
            if (cmd.Value is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
