//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

using Calculator.Configuration;
using Calculator.Messages;
using Calculator.Web;

using CalculatorShell.Core;
using CalculatorShell.Core.Mediator;

namespace Calculator;

internal sealed class TerminalHost : IHost, IWritableHost
{
    private readonly TerminalInput _input;
    private readonly TerminalOutput _output;
    private readonly ICurrentDirectoryProvider _currentDirectoryProvider;
    private CultureInfo _culture;

    public TerminalHost(ICurrentDirectoryProvider currentDirectoryProvider)
    {
        _currentDirectoryProvider = currentDirectoryProvider;
        _culture = CultureInfo.InvariantCulture;
        _input = new TerminalInput();
        _output = new TerminalOutput(ConfigAccessor);
        Mediator = new Mediator();
        WebServices = new WebServices();
        Log = new MemoryLog();
        Dialogs = new TerminalDialogs(this);
    }

    private Config ConfigAccessor()
    {
        return Mediator.Request<Config, ConfigRequest>(new ConfigRequest())
            ?? throw new InvalidOperationException("Config was not found");
    }

    public ITerminalInput Input => _input;

    public ITerminalOutput Output => _output;

    public CultureInfo CultureInfo => _culture;

    public IMediator Mediator { get; }

    public IWebServices WebServices { get; }

    public IDialogs Dialogs { get; }

    public ILog Log { get; }

    public string CurrentDirectory
        => _currentDirectoryProvider.CurrentDirectory;

    string IWritableHost.CurrentDirectory
    {
        get => _currentDirectoryProvider.CurrentDirectory;
        set => _currentDirectoryProvider.CurrentDirectory = value;
    }

    CultureInfo IWritableHost.CultureInfo
    {
        get => _culture;
        set => _culture = value;
    }

    public DateTime Now()
        => DateTime.Now;

    void IWritableHost.SetCommandData(IReadOnlyDictionary<string, string> commandHelps, IReadOnlyDictionary<string, IArgumentCompleter> completers, ISet<string> exitCommands)
    {
        _input.SetCommandData(commandHelps, completers, exitCommands);
    }
}
