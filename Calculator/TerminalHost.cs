using System.Globalization;

using Calculator.Web;

using CalculatorShell.Core;
using CalculatorShell.Core.Mediator;

namespace Calculator;

internal sealed class TerminalHost : IHost, IHelpDataSetter
{
    private readonly TerminalInput _input;
    private readonly TerminalOutput _output;

    public TerminalHost()
    {
        _input = new TerminalInput();
        _output = new TerminalOutput();
        Mediator = new Mediator();
        WebServices = new WebServices();
        Dialogs = new TerminalDialogs();
        Log = new MemoryLog();
    }

    public ITerminalInput Input => _input;

    public ITerminalOutput Output => _output;

    public CultureInfo CultureInfo
    {
        get => _input.CultureInfo;
        set
        {
            _input.CultureInfo = value;
            _output.CultureInfo = value;
        }
    }

    public IMediator Mediator { get; }

    public IWebServices WebServices { get; }

    public IDialogs Dialogs { get; }

    public ILog Log { get; }

    public string CurrentDirectory 
        => Environment.CurrentDirectory;

    public void SetCommandData(IReadOnlyDictionary<string, string> commandHelps,
                                 IReadOnlyDictionary<string, IArgumentCompleter> completers,
                                 ISet<string> exitCommands)
        => _input.SetCommandData(commandHelps, completers, exitCommands);
}
