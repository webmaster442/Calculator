using System.Globalization;

using Calculator.Web;

using CalculatorShell.Core;
using CalculatorShell.Core.Messenger;

namespace Calculator;

internal sealed class TerminalHost : IHost
{
    private readonly TerminalInput _input;
    private readonly TerminalOutput _output;

    public TerminalHost()
    {
        _input = new TerminalInput();
        _output = new TerminalOutput();
        MessageBus = new MessageBus();
        WebServices = new WebServices();
        Dialogs = new TerminalDialogs();
    }

    public ITerminalInput Input => _input;

    public ITerminalOutput Output => _output;

    public string Prompt
    {
        get => _input.Prompt;
        set => _input.Prompt = value;
    }

    public CultureInfo CultureInfo
    {
        get => _input.CultureInfo;
        set
        {
            _input.CultureInfo = value;
            _output.CultureInfo = value;
        }
    }

    public IMessageBus MessageBus { get; }

    public IWebServices WebServices { get; }

    public IDialogs Dialogs { get; }

    internal void SetCommandData(IReadOnlyDictionary<string, string> commandHelps, HashSet<string> exitCommands)
    {
        _input.SetCommandData(commandHelps, exitCommands);
    }
}
