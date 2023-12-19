using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Messenger;
using CalculatorShell.Engine;

namespace Calculator.Commands;

internal class EvalCommand : ShellCommandAsync,
    IMessageClient<SimpleMessage<AngleSystem>>,
    IMessageClient<SetVarMessage>,
    IMessageClient<UnsetVarMessage>,
    IMessageProvider<IEnumerable<string>, FunctionListMessage>
{
    private readonly ArithmeticEngine _engine;
    private readonly Varialbes _varialbes;

    public EvalCommand(IHost host) : base(host)
    {
        ClientId = new Guid("F38F1CFB-D8D5-434F-81EA-DAD47C3AFF85");
        _varialbes = new Varialbes();
        _engine = new ArithmeticEngine(_varialbes, host.CultureInfo);
        Host.MessageBus.RegisterComponent(this);
    }

    public override string[] Names => ["$", "eval", "calc"];

    public Guid ClientId { get; }

    public override async Task Execute(Arguments args, CancellationToken cancellationToken)
    {
        EngineResult result = await _engine.ExecuteAsync(string.Join(' ', args.AsEnumerable()), cancellationToken);
        result.When(number => Host.Output.Result(number.ToString(Host.CultureInfo)),
                    exception => Host.Output.Error(exception));
    }

    void IMessageClient<SimpleMessage<AngleSystem>>.ProcessMessage(SimpleMessage<AngleSystem> message)
        => _engine.AngleSystem = message.Payload;

    async void IMessageClient<SetVarMessage>.ProcessMessage(SetVarMessage message)
    {
        EngineResult result = await _engine.ExecuteAsync(message.Expression, CancellationToken.None);
        result.When(number => _varialbes.Set(message.VariableName, number),
            exception => throw exception);
    }

    void IMessageClient<UnsetVarMessage>.ProcessMessage(UnsetVarMessage message)
        => _varialbes.Unset(message.VariableName);

    IEnumerable<string> IMessageProvider<IEnumerable<string>, FunctionListMessage>.ProvideMessage(FunctionListMessage request)
        => _engine.Functions;
}
