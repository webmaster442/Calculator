using Calculator.Internal;
using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Mediator;
using CalculatorShell.Engine;

using PrettyPrompt.Highlighting;

namespace Calculator;
internal sealed class App :
    IDisposable,
    INotifyTarget<AngleSystemChange>,
    INotifyTarget<SetCurrentDir>,
    INotifyTarget<EnqueCommands>,
    INotifyTarget<SetOptions>,
    IRequestProvider<IEnumerable<string>, CommandList>,
    IRequestProvider<Options, OptionsRequest>
{
    private readonly IHost _host;
    private readonly CommandLoader _loader;
    private readonly Expenses _expenses;
    private readonly HashSet<string> _exitCommands;
    private readonly ITerminalInput _input;
    private readonly TimeProvider _timeProvider;
    private readonly ICurrentDirectoryProvider _currentDirectoryProvider;
    private CancellationTokenSource? _currentTokenSource;

    private AngleSystem _angleSystem;
    private Queue<string> _commandQue;
    private Options _options;

    public App(IHost host, 
               ITerminalInput input, 
               IHelpDataSetter uiDataSetter,
               TimeProvider timeProvider,
               ICurrentDirectoryProvider currentDirectoryProvider)
    {
        _options = new Options();
        _commandQue = new Queue<string>();
        _host = host;
        _loader = new(typeof(App), _host);
        _expenses = new Expenses(_host);
        _exitCommands = ["exit", "quit"];
        _host.Mediator.Register(this);
        _input = input;
        _timeProvider = timeProvider;
        _currentDirectoryProvider = currentDirectoryProvider;
        uiDataSetter.SetCommandData(_loader.CommandHelps, _loader.CompletableCommands, _exitCommands);
    }

    public async Task Run(bool singleRun = false)
    {
        bool run = true;

        ExecuteAutoRuns();

        _input.Prompt = CreatePrompt();

        while (run)
        {
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
                    if (!singleRun)
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
                    if (!singleRun)
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
            _input.Prompt = CreatePrompt();

            if (singleRun)
                run = false;
        }
    }

    private FormattedString CreatePrompt()
    {
        string timeString = _timeProvider.GetLocalNow().DateTime.ToShortTimeString();
        string text = $"Calc ({_angleSystem}) | {timeString}\r\n{_currentDirectoryProvider.CurrentDirectory} >";
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

        return _input.ReadLine();
    }

    private void ExecuteAutoRuns()
    {
        foreach (var cmd in _loader.AutoExecCommands.OrderBy(x => x.Priority))
        {
            _host.Log.Info($"{cmd.LogMessage}");
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

    void INotifyTarget<AngleSystemChange>.OnNotify(AngleSystemChange message)
        => _angleSystem = message.AngleSystem;

    void INotifyTarget<SetCurrentDir>.OnNotify(SetCurrentDir message)
    {
        try
        {
            var info = new DirectoryInfo(message.CurrentFolder);
            if (info.LinkTarget != null)
                _currentDirectoryProvider.CurrentDirectory = info.LinkTarget;
            else
                _currentDirectoryProvider.CurrentDirectory = message.CurrentFolder;
        }
        catch (Exception ex)
        {
            _host.Log.Exception(ex);
            _host.Output.Error($"Can't navigate to: {message.CurrentFolder}");
        }
    }

    void INotifyTarget<EnqueCommands>.OnNotify(EnqueCommands message)
        => _commandQue = new Queue<string>(message.Commands);

    void INotifyTarget<SetOptions>.OnNotify(SetOptions message)
        => _options = message.Options;

    IEnumerable<string> IRequestProvider<IEnumerable<string>, CommandList>.OnRequest(CommandList message)
        => _loader.Commands.Keys.Concat(_exitCommands);

    Options IRequestProvider<Options, OptionsRequest>.OnRequest(OptionsRequest message)
        => _options;
}
