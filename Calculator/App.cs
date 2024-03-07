//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

using Calculator.AutoRun;
using Calculator.Internal;
using Calculator.Messages;
using Calculator.Web.Server;

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
    IRequestProvider<Options, OptionsRequest>,
    IRequestProvider<string, HelpRequestMessage>
{
    private readonly IHost _host;
    private readonly ITerminalInput _input;
    private readonly ICurrentDirectoryProvider _currentDirectoryProvider;
    private readonly TimeProvider _timeProvider;

    private readonly CommandLoader _loader;
    private readonly HashSet<string> _exitCommands;

    private readonly Expenses _expenses;
    private readonly HttpServer _server;
    private readonly History _history;

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
        _server = new HttpServer(_host.Log);
        _history = new History(_host.Mediator);
    }

    public async Task Run(bool singleRun = false)
    {
        bool run = true;

        _expenses.RegisterToMediator();
        _history.RegisterToMediator();
        ExecuteAutoRuns(singleRun);
        _input.Prompt = CreatePrompt();

        StartServer();

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
                    _host.Log.Info($"Executing: {cmdAndArgs.cmd} {cmdAndArgs.Arguments.Text}");
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
                    _host.Mediator.Notify(new AddHistory($"{cmdAndArgs.cmd} {cmdAndArgs.Arguments.Text}"));
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

    private void StartServer()
    {
        var handlers = CommandLoader.LoadAdditionalTypes<IRequestHandler>(typeof(App), _host);
        int port = _server.Start(handlers);
        _host.Mediator.Notify(new HttpServerPort(port));
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
            return ArgumentsFactory.Create(_commandQue.Dequeue());

        return _input.ReadLine();
    }

    private void ExecuteAutoRuns(bool singleRun)
    {
        foreach (var cmd in CommandLoader.LoadAdditionalTypes<IAutoExec>(typeof(App), _host).OrderBy(x => x.Priority))
        {
            if (cmd is IntroAutoExec && singleRun)
                continue;

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
        _server.Dispose();
    }

    void INotifyTarget<AngleSystemChange>.OnNotify(AngleSystemChange message)
        => _angleSystem = message.AngleSystem;

    void INotifyTarget<SetCurrentDir>.OnNotify(SetCurrentDir message)
    {
        try
        {
            var info = new DirectoryInfo(message.CurrentFolder);
            _currentDirectoryProvider.CurrentDirectory = info.LinkTarget ?? message.CurrentFolder;
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

    string IRequestProvider<string, HelpRequestMessage>.OnRequest(HelpRequestMessage message)
    {
        var cmd = _loader.Commands
            .Where(x => string.Compare(x.Key, message.Command, true) == 0)
            .Select(x => new
        {
            x.Value.Synopsys,
            HelpMessage = x.Value.HelpMessage.TrimEnd()
        }).FirstOrDefault();

        if (cmd == null)
            return $"No help was found for command: {message.Command}";

        StringBuilder final = new();
        final.AppendLine(cmd.Synopsys);
        final.AppendLine();
        final.Append(cmd.HelpMessage);

        if (cmd.HelpMessage == ArgumentExtensions.ArgumentHeader)
        {
            final.AppendLine(" This command doesn't require any parameters");
        }

        return final.ToString();
    }
}
