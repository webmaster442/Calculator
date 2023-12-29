using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Messenger;
using CalculatorShell.Engine;

namespace Calculator.Commands;

internal class EvalCommand : ShellCommandAsync,
    IMessageClient<SimpleMessage<AngleSystem>>,
    IMessageClient<SetVarMessage>,
    IMessageClient<UnsetVarMessage>,
    IMessageProvider<IEnumerable<string>, FunctionListMessage>,
    IMessageProvider<IEnumerable<KeyValuePair<string, Number>>, VariableListMessage>
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

    public override string[] Names => ["$", "eval"];

    public override string Synopsys
        => "Evaluate an expression and writes out the result";

    public Guid ClientId { get; }

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
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
        result.When(number => 
        {
            try
            {
                _varialbes.Set(message.VariableName, number);
            }
            catch (Exception ex)
            {
                Host.Output.Error(ex);
                Host.Output.BlankLine();
            }
        },
        exception => Host.Output.Error(exception));
    }

    void IMessageClient<UnsetVarMessage>.ProcessMessage(UnsetVarMessage message)
    {
        try
        {
            _varialbes.Unset(message.VariableName);
        }
        catch (Exception ex)
        {
            Host.Output.Error(ex);
        }
    }

    IEnumerable<string> IMessageProvider<IEnumerable<string>, FunctionListMessage>.ProvideMessage(FunctionListMessage request)
        => _engine.Functions;

    IEnumerable<KeyValuePair<string, Number>> IMessageProvider<IEnumerable<KeyValuePair<string, Number>>, VariableListMessage>.ProvideMessage(VariableListMessage request)
        => _varialbes.AsEnumerable();
}
