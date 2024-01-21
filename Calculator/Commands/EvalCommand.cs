using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Mediator;
using CalculatorShell.Engine;

namespace Calculator.Commands;

internal class EvalCommand : ShellCommandAsync,
    INotifyTarget<AngleSystemMessage>,
    INotifyTarget<SetVarMessage>,
    INotifyTarget<UnsetVarMessage>,
    IRequestProvider<IEnumerable<string>, FunctionListRequestMessage>,
    IRequestProvider<IEnumerable<KeyValuePair<string, Number>>, VariableListMessage>
{
    private readonly ArithmeticEngine _engine;
    private readonly Varialbes _varialbes;

    public EvalCommand(IHost host) : base(host)
    {
        _varialbes = new Varialbes();
        _engine = new ArithmeticEngine(_varialbes, host.CultureInfo);
        Host.Mediator.Register(this);
    }

    public override string[] Names => ["$", "eval"];

    public override string Synopsys
        => "Evaluate an expression and writes out the result";

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        EngineResult result = await _engine.ExecuteAsync(string.Join(' ', args.AsEnumerable()), cancellationToken);
        result.When(number => Host.Output.Result(number.ToString(Host.CultureInfo)),
                    exception =>
                    {
                        Host.Log.Exception(exception);
                        Host.Output.Error(exception);
                    });
    }

    void INotifyTarget<AngleSystemMessage>.OnNotify(AngleSystemMessage message)
        => _engine.AngleSystem = message.AngleSystem;

    void INotifyTarget<SetVarMessage>.OnNotify(SetVarMessage message)
    {
        EngineResult result = _engine.ExecuteAsync(message.Expression, CancellationToken.None).GetAwaiter().GetResult();
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

    void INotifyTarget<UnsetVarMessage>.OnNotify(UnsetVarMessage message)
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

    IEnumerable<string> IRequestProvider<IEnumerable<string>, FunctionListRequestMessage>.OnRequest(FunctionListRequestMessage message)
        => _engine.Functions;

    IEnumerable<KeyValuePair<string, Number>> IRequestProvider<IEnumerable<KeyValuePair<string, Number>>, VariableListMessage>.OnRequest(VariableListMessage message)
        => _varialbes.AsEnumerable();
}
