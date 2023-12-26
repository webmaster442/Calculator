using System.Data;

namespace CalculatorShell.Core;

public sealed class CommandLoader
{
    private readonly Dictionary<string, IShellCommand> _commands;
    private readonly Dictionary<string, string> _commandHelps;
    private readonly List<IAutoExecShellCommand> _autoExecCommands;

    public IReadOnlyList<IAutoExecShellCommand> AutoExecCommands
        => _autoExecCommands;

    public IReadOnlyDictionary<string, IShellCommand> Commands
        => _commands;

    public IReadOnlyDictionary<string, string> CommandHelps
        => _commandHelps;

    public CommandLoader(Type atypeFromAssembly, IHost host)
    {
        _commands = new Dictionary<string, IShellCommand>();
        _commandHelps = new Dictionary<string, string>();
        _autoExecCommands = new List<IAutoExecShellCommand>();
        LoadCommands(atypeFromAssembly, host);
        LoadAutoExecCommands(atypeFromAssembly, host);
    }

    private void LoadAutoExecCommands(Type atypeFromAssembly, IHost host)
    {
        IEnumerable<Type> autoExecCommandTypes = GetTypesFromAssembly<IAutoExecShellCommand>(atypeFromAssembly);
        foreach (Type type in autoExecCommandTypes) 
        {
            try
            {
                if (Activator.CreateInstance(type) is IAutoExecShellCommand cmd)
                {
                    _autoExecCommands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                host.Output.Error(ex);
            }
        }
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
}
