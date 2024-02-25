//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Data;

namespace CalculatorShell.Core;

public sealed class CommandLoader : IDisposable
{
    private readonly Dictionary<string, IShellCommand> _commands;
    private readonly Dictionary<string, string> _commandHelps;
    private readonly Dictionary<string, IArgumentCompleter> _completable;

    public IReadOnlyDictionary<string, IShellCommand> Commands
        => _commands;

    public IReadOnlyDictionary<string, string> CommandHelps
        => _commandHelps;

    public IReadOnlyDictionary<string, IArgumentCompleter> CompletableCommands
        => _completable;


    public CommandLoader(Type atypeFromAssembly, IHost host)
    {
        _commands = new Dictionary<string, IShellCommand>();
        _commandHelps = new Dictionary<string, string>();
        _completable = new Dictionary<string, IArgumentCompleter>();
        LoadCommands(atypeFromAssembly, host);
    }

    public static IReadOnlyList<TType> LoadAdditionalTypes<TType>(Type atypeFromAssembly, IHost host)
        where TType : class
    {
        var types = GetTypesFromAssembly<TType>(atypeFromAssembly);
        List<TType> result = new List<TType>();
        foreach (var type in types)
        {
            try
            {
                if (Activator.CreateInstance(type) is TType cmd)
                {
                    result.Add(cmd);
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

    public void Dispose()
    {
        foreach (var cmd in _commands)
        {
            if (cmd.Value is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
