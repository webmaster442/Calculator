//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Configuration;
using Calculator.Internal;
using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Mediator;
using CalculatorShell.Engine;

namespace Calculator.Commands;

internal class EvalCommand : ShellCommandAsync,
    INotifyTarget<AngleSystemChange>,
    INotifyTarget<SetVariable>,
    INotifyTarget<UnsetVariable>,
    IRequestProvider<IEnumerable<string>, FunctionListRequest>,
    IRequestProvider<IEnumerable<KeyValuePair<string, Number>>, VariableListRequest>
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

    public override string Category
        => CommandCategories.Calculation;

    public override string Synopsys
        => "Evaluate an expression and writes out the result";

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        var options = Host.Mediator.Request<Config, ConfigRequest>(new ConfigRequest())
            ?? throw new InvalidOperationException("Couldn't get configuration");

        EngineResult result = await _engine.ExecuteAsync(args.Text, cancellationToken);
        result.When(number =>
        {
            Host.Output.Result(NumberFomatter.ToString(number, Host.CultureInfo, options.ThousandGroupping));
            _engine.Variables.Set("ans", number);
        },
        exception =>
        {
            Host.Log.Exception(exception);
            Host.Output.Error(exception);
        });
    }

    void INotifyTarget<AngleSystemChange>.OnNotify(AngleSystemChange message)
        => _engine.AngleSystem = message.AngleSystem;

    void INotifyTarget<SetVariable>.OnNotify(SetVariable message)
    {
        EngineResult result = _engine.ExecuteAsync(message.Expression, CancellationToken.None).GetAwaiter().GetResult();
        result.When(number =>
        {
            try
            {
                if (message.VariableName == "ans")
                    throw new CommandException("ans can't be written directly");

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

    void INotifyTarget<UnsetVariable>.OnNotify(UnsetVariable message)
    {
        try
        {
            if (message.VariableName == "-+all+-")
                _varialbes.UnsetAll();

            _varialbes.Unset(message.VariableName);
        }
        catch (Exception ex)
        {
            Host.Output.Error(ex);
        }
    }

    IEnumerable<string> IRequestProvider<IEnumerable<string>, FunctionListRequest>.OnRequest(FunctionListRequest message)
        => _engine.Functions;

    IEnumerable<KeyValuePair<string, Number>> IRequestProvider<IEnumerable<KeyValuePair<string, Number>>, VariableListRequest>.OnRequest(VariableListRequest message)
        => _varialbes.AsEnumerable();
}
