//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;
using CalculatorShell.Engine;

using CommandLine;

namespace Calculator.Commands;
internal class Simplify : ShellCommand
{
    private readonly LogicEngine _engine;

    public Simplify(IHost host) : base(host)
    {
        _engine = new LogicEngine();
    }

    public override string[] Names => ["simplify"];

    public override string Category
        => CommandCategories.Calculation;

    public override string Synopsys
        => "Simplifies a logical expression";

    public override string HelpMessage
        => this.BuildHelpMessage<SimplifyOptions>();

    internal class SimplifyOptions
    {
        [Option('v', "variables", HelpText = "Variable count of function, in minterm mode only", SetName = "miterm")]
        public int VariableCount { get; set; }

        [Option('m', "minterms", HelpText = "Minterm numbers, where the function is true. -v must be also specified", SetName = "miterm")]
        public IEnumerable<int> Minterms { get; set; }

        [Option('e', "expression", HelpText = "Expression to simplify", SetName = "expression")]
        public string? Expression { get; set; }

        public SimplifyOptions()
        {
            Minterms = Enumerable.Empty<int>();
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<SimplifyOptions>(Host);

        if (options.Expression == null && options.Minterms.Any())
        {
            var result = _engine.Parse(options.VariableCount, options.Minterms.ToList());
            Host.Output.Result(result.ToString(Host.CultureInfo));
        }
        else if (!string.IsNullOrEmpty(options.Expression))
        {
            var result = _engine.Parse(options.Expression).Simplify();
            Host.Output.Result(result.ToString(Host.CultureInfo));
        }
        else
        {
            throw new CommandException("No minterms or expression was given");
        }
    }
}
