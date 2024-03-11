using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents.Stat;

using CommandLine;

namespace Calculator.Commands;
internal class StatCommand : ShellCommand
{
    public StatCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["stat"];

    public override string Category
        => CommandCategories.Calculation;

    public override string Synopsys
        => "Computes statistical information of given numbers";

    public override string HelpMessage
        => this.BuildHelpMessage<StatOptions>();

    internal class StatOptions
    {
        [Value(0, HelpText = "Numbers that will be used to calculate statistics", Required = true)]
        public IEnumerable<double> Numbers { get; set; }

        public StatOptions()
        {
            Numbers = Enumerable.Empty<double>();
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<StatOptions>(Host);
        var result = Statistics.Compute(options.Numbers);
        Host.Output.Result(result.ToString());
    }
}
