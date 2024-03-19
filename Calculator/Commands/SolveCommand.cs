//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;
using CommandLine;

using static Calculator.Commands.Simplify;

namespace Calculator.Commands;
internal class SolveCommand : ShellCommand
{
    public SolveCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["solve"];

    public override string Category 
        => CommandCategories.Calculation;

    public override string Synopsys
        => """
        Equation solver up to the 4th order.
        Equation must be in form of: x^4 + x^3 + x^2 + x + c = 0
        """;

    public override string HelpMessage
        => this.BuildHelpMessage<SolveOptions>();

    public override IArgumentCompleter? ArgumentCompleter
        => new OptionClassCompleter<SolveOptions>(Host);

    internal class SolveOptions
    {
        [Option("x0", HelpText = ">The constant term. (X^0)")]
        public double X0 { get; set; }

        [Option("x1", HelpText = ">Coefficient of x^1")]
        public double X1 { get; set; }

        [Option("x2", HelpText = ">Coefficient of x^2")]
        public double X2 { get; set; }

        [Option("x3", HelpText = ">Coefficient of x^3")]
        public double X3 { get; set; }

        [Option("x4", HelpText = ">Coefficient of x^4")]
        public double X4 { get; set; }
    }

    public override void ExecuteInternal(Arguments args)
    {
        if (args.Count == 0)
            throw new CommandException("No arguments were given");

        var options = args.Parse<SolveOptions>(Host);
        var roots = EquationSolver.FindRoots(options.X4, options.X3, options.X2, options.X1, options.X0);
        Host.Output.Result(roots);

    }
}
