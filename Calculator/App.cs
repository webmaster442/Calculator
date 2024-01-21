using Calculator.Internal;
using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Messenger;
using CalculatorShell.Engine;

using PrettyPrompt.Highlighting;

namespace Calculator;
internal sealed class App :
    IDisposable,
    IMessageClient<SimpleMessage<AngleSystem>>,
    IMessageClient<CurrentDirMessage>,
    IMessageClient<SimpleMessage<string[]>>,
    IMessageProvider<string, RequestCurrentDirMessage>,
    IMessageProvider<IEnumerable<string>, CommandListMessage>
{
    private readonly TerminalHost _host;
    private readonly CommandLoader _loader;
    private readonly Expenses _expenses;
    private readonly HashSet<string> _exitCommands;
    private CancellationTokenSource? _currentTokenSource;

    private AngleSystem _angleSystem;
    private Queue<string> _commandQue;

    public Guid ClientId { get; }

    public App()
    {
        Environment.CurrentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        ClientId = new Guid("1D386507-929F-42F7-8DDB-8E038F6A85F6");
        _commandQue = new Queue<string>();
        _host = new TerminalHost();
        _loader = new(typeof(App), _host);
        _expenses = new Expenses(_host);
        _exitCommands = ["exit", "quit"];
        _host.SetCommandData(_loader.CommandHelps, _loader.CompletableCommands, _exitCommands);
        _host.MessageBus.RegisterComponent(this);
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

    void IMessageClient<SimpleMessage<AngleSystem>>.ProcessMessage(SimpleMessage<AngleSystem> message)
        => _angleSystem = message.Payload;

    IEnumerable<string> IMessageProvider<IEnumerable<string>, CommandListMessage>.ProvideMessage(CommandListMessage request)
        => _loader.Commands.Keys.Concat(_exitCommands);

    void IMessageClient<CurrentDirMessage>.ProcessMessage(CurrentDirMessage input)
        => Environment.CurrentDirectory = input.CurrentFolder;

    string IMessageProvider<string, RequestCurrentDirMessage>.ProvideMessage(RequestCurrentDirMessage request)
        => Environment.CurrentDirectory;

    void IMessageClient<SimpleMessage<string[]>>.ProcessMessage(SimpleMessage<string[]> input)
        => _commandQue = new Queue<string>(input.Payload);
}
