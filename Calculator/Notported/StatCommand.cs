//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents.Stat;

namespace Calculator.Notported;
/*internal class StatCommand : ShellCommand
{
    public StatCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["stat"];

    public override string Synopsys
        => "Computes statistical data on a set of numbers";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(1);

        var numbers = args.AsEnumerable().Select(x =>
        {
            if (double.TryParse(x, Host.CultureInfo, out double parsed))
            {
                return parsed;
            }
            return (double?)null;
        }).Where(x => x.HasValue)
        .Select(x => x!.Value);

        var result = Statistics.Compute(numbers);
        Host.Output.Result(result.ToString());
    }
}*/
