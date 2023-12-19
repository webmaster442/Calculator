using CalculatorShell.Core;
using CalculatorShell.Engine;

namespace Calculator.Commands;

internal sealed class SimplifyCommand : ShellCommand
{
    private readonly ILogicEngine _engine;

    public SimplifyCommand(IHost host) : base(host)
    {
        _engine = new LogicEngine();
    }

    public override string[] Names => ["simplify"];

    public override void Execute(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(1);

        if (args.TryParseAll<int>(out int[] mintemMode))
        {
            var result = _engine.Parse(mintemMode.First(), mintemMode.Skip(1).ToArray());
            Host.Output.Result(result.ToString(Host.CultureInfo));
        }
        else
        {
            var expression = string.Join(' ', args);
            var result = _engine.Parse(expression).Simplify();
            Host.Output.Result(result.ToString(Host.CultureInfo));
        }
    }
}
