//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

using Calculator.Web;

using CalculatorShell.Core;
using CalculatorShell.Core.Mediator;

namespace Calculator;

internal sealed class TerminalHost : IHost, IHelpDataSetter
{
    private readonly TerminalInput _input;
    private readonly TerminalOutput _output;
    private readonly ICurrentDirectoryProvider _currentDirectoryProvider;

    public TerminalHost(ICurrentDirectoryProvider currentDirectoryProvider)
    {
        _currentDirectoryProvider = currentDirectoryProvider;
        CultureInfo = CultureInfo.InvariantCulture;
        _input = new TerminalInput();
        _output = new TerminalOutput();
        Mediator = new Mediator();
        WebServices = new WebServices();
        Log = new MemoryLog();
        Dialogs = new TerminalDialogs(this);
    }

    public ITerminalInput Input => _input;

    public ITerminalOutput Output => _output;

    public CultureInfo CultureInfo
    {
        get;
    }

    public IMediator Mediator { get; }

    public IWebServices WebServices { get; }

    public IDialogs Dialogs { get; }

    public ILog Log { get; }

    public string CurrentDirectory
        => _currentDirectoryProvider.CurrentDirectory;

    public DateTime Now() 
        => DateTime.Now;

    public void SetCommandData(IReadOnlyDictionary<string, string> commandHelps,
                                 IReadOnlyDictionary<string, IArgumentCompleter> completers,
                                 ISet<string> exitCommands)
        => _input.SetCommandData(commandHelps, completers, exitCommands);
}
