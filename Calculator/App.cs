using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Messenger;
using CalculatorShell.Engine;

namespace Calculator;
internal sealed class App :
    IDisposable,
    IMessageClient<SimpleMessage<AngleSystem>>,
    IMessageProvider<IEnumerable<string>, CommandListMessage>
{
    private readonly TerminalHost _host;
    private readonly CommandLoader _loader;
    private readonly HashSet<string> _exitCommands;
    private CancellationTokenSource? _currentTokenSource;

    private AngleSystem _angleSystem;

    public Guid ClientId { get; }

    public App()
    {
        ClientId = new Guid("1D386507-929F-42F7-8DDB-8E038F6A85F6");
        _host = new TerminalHost();
        _loader = new(typeof(App), _host);
        _exitCommands = ["exit", "quit"];

        _host.SetCommandData(_loader.CommandHelps, _exitCommands);
        _host.MessageBus.RegisterComponent(this);


    }

    public async Task Run()
    {
        bool run = true;

        AutoRun();

        while (run)
        {
            _host.Prompt = $"Calc ({_angleSystem})> ";
            var cmdAndArgs = _host.Input.ReadLine();
            if (_exitCommands.Contains(cmdAndArgs.cmd))
            {
                run = false;
                break;
            }
            if (_loader.Commands.ContainsKey(cmdAndArgs.cmd))
            {
                try
                {
                    Console.CancelKeyPress += OnCancelKeyPress;
                    _currentTokenSource = new CancellationTokenSource();

                    var command = _loader.Commands[cmdAndArgs.cmd];

                    if (command is ISyncShellCommand sync)
                        sync.Execute(cmdAndArgs.Arguments);
                    else if (command is IAsyncShellCommand async)
                        await async.Execute(cmdAndArgs.Arguments, _currentTokenSource.Token);
                    else
                        throw new InvalidOperationException($"Can't execute: {cmdAndArgs.cmd}");

                    _host.Output.BlankLine();
                }
                finally
                {
                    Console.CancelKeyPress -= OnCancelKeyPress;
                    _currentTokenSource?.Cancel();
                    _currentTokenSource = null;
                }
            }
            else
            {
                _host.Output.Error($"Unknown Command: {cmdAndArgs.cmd}");
            }
        }
    }

    private void AutoRun()
    {
        foreach (var cmd in _loader.AutoExecCommands)
        {
            cmd.Execute(_host);
        }
    }

    private void OnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        _currentTokenSource?.Cancel();
    }

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
    {
        _angleSystem = message.Payload;
    }

    IEnumerable<string> IMessageProvider<IEnumerable<string>, CommandListMessage>.ProvideMessage(CommandListMessage request)
    {
        return _loader.Commands.Keys.Concat(_exitCommands);
    }
}
