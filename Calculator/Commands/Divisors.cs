using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;
internal class Divisors : ShellCommandAsync
{
    public Divisors(IHost host) : base(host)
    {
    }

    public override string[] Names => ["divisors"];

    public override string Category 
        => CommandCategories.Calculation;

    public override string Synopsys 
        => "Return the divisors of a given iteger number";

    public override string HelpMessage 
        => throw new NotImplementedException();

    internal class DivisorsOptions
    {
        [Value(0, HelpText = "Number, whose divisors will be returned", Required = true)]
        public Int128 Number { get; set; }
    }

    private Task<IOrderedEnumerable<Int128>> GetDivisors(Int128 number, CancellationToken cancellationToken)
    {
        List<Int128> divisors = new();
        for (Int128 i = 1; (i * i) <= number; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (number % i == 0)
            {
                divisors.Add(i);
                if (i != number / i)
                {
                    divisors.Add(number / i);
                }
            }
        }
        return Task.FromResult(divisors.Order());
    }

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        var options = args.Parse<DivisorsOptions>(Host);
        var number = Int128.Abs(options.Number);

        var divisors = await GetDivisors(number, cancellationToken);

        Host.Output.Result(string.Join(", ", divisors));
    }
}
