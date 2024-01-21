using Calculator.Internal;
using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Mediator;
using CalculatorShell.Engine;

using PrettyPrompt.Highlighting;

namespace Calculator;
internal sealed class App :
    IDisposable,
    INotifyTarget<AngleSystemMessage>,
    INotifyTarget<CurrentDirMessage>,
    INotifyTarget<EnqueCommandsMessage>,
    IRequestProvider<string, RequestCurrentDirMessage>,
    IRequestProvider<IEnumerable<string>, CommandListMessage>
{
    private readonly TerminalHost _host;
    private readonly CommandLoader _loader;
    private readonly Expenses _expenses;
    private readonly HashSet<string> _exitCommands;
    private CancellationTokenSource? _currentTokenSource;

    private AngleSystem _angleSystem;
    private Queue<string> _commandQue;

    public App()
    {
        Environment.CurrentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        _commandQue = new Queue<string>();
        _host = new TerminalHost();
        _loader = new(typeof(App), _host);
        _expenses = new Expenses(_host);
        _exitCommands = ["exit", "quit"];
        _host.SetCommandData(_loader.CommandHelps, _loader.CompletableCommands, _exitCommands);
        _host.Mediator.Register(this);
    }

    public async Task Run()
    {
        bool run = true;

        AutoRun();

        while (run)
        {
            _host.Prompt = CreatePrompt();
            var cmdAndArgs = GetCommandAndArgs();
            if (_exitCommands.Contains(cmdAndArgs.cmd))
            {
                run = false;
                break;
            }
            if (_loader.Commands.ContainsKey(cmdAndArgs.cmd))
            {
                try
                {
                    _host.Log.Info($"Executing: {cmdAndArgs.cmd} {string.Join(' ', cmdAndArgs.Arguments.AsEnumerable())}");
                    Console.CancelKeyPress += OnCancelKeyPress;
                    _currentTokenSource = new CancellationTokenSource();

                    var command = _loader.Commands[cmdAndArgs.cmd];

                    if (command is ISyncShellCommand sync)
                        sync.Execute(cmdAndArgs.Arguments);
                    else if (command is IAsyncShellCommand async)
                        await async.Execute(cmdAndArgs.Arguments, _currentTokenSource.Token);
                    else
                        throw new InvalidOperationException($"Can't execute: {cmdAndArgs.cmd}");
                }
                finally
                {
                    Console.CancelKeyPress -= OnCancelKeyPress;
                    _currentTokenSource?.Cancel();
                    _currentTokenSource = null;
                }
            }
            else if (cmdAndArgs.cmd.Length > 0)
            {
                _host.Output.Error($"Unknown Command: {cmdAndArgs.cmd}");
                _host.Log.Error($"Unknown Command: {cmdAndArgs.cmd}");
                if (_commandQue.Count > 0)
                {
                    _commandQue.Clear();
                    _host.Output.Error("Execution stopped");
                }
            }
            _host.Output.BlankLine();
        }
    }

    private FormattedString CreatePrompt()
    {
        string text = $"Calc ({_angleSystem}) | {DateTime.Now.ToShortTimeString()}\r\n{Environment.CurrentDirectory} >";
        int linesplit = text.IndexOf('\n');
        int secondLine = text.Length - linesplit - " >".Length;
        return new FormattedString(text,
                                   new FormatSpan(0, linesplit, new ConsoleFormat(AnsiColor.White)),
                                   new FormatSpan(linesplit, secondLine, new ConsoleFormat(AnsiColor.BrightMagenta, Underline: true)));
    }

    private (string cmd, Arguments Arguments) GetCommandAndArgs()
    {
        if (_commandQue.Count > 0)
            return ArgumentsFactory.Create(_commandQue.Dequeue(), _host.CultureInfo);

        return _host.Input.ReadLine();
    }

    private void AutoRun()
    {
        foreach (var cmd in _loader.AutoExecCommands)
        {
            cmd.Execute(_host);
        }
    }

    private void OnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        => _currentTokenSource?.Cancel();

    public void Dispose()
    {
        if (_currentTokenSource != null)
        {
            _currentTokenSource.Dispose();
            _currentTokenSource = null;
        }
        _loader.Dispose();
    }

    void INotifyTarget<AngleSystemMessage>.OnNotify(AngleSystemMessage message)
        => _angleSystem = message.AngleSystem;

    void INotifyTarget<CurrentDirMessage>.OnNotify(CurrentDirMessage message)
        => Environment.CurrentDirectory = message.CurrentFolder;

    void INotifyTarget<EnqueCommandsMessage>.OnNotify(EnqueCommandsMessage message)
        => _commandQue = new Queue<string>(message.Commands);

    IEnumerable<string> IRequestProvider<IEnumerable<string>, CommandListMessage>.OnRequest(CommandListMessage message)
        => _loader.Commands.Keys.Concat(_exitCommands);

    string IRequestProvider<string, RequestCurrentDirMessage>.OnRequest(RequestCurrentDirMessage message)
        => Environment.CurrentDirectory;
}
