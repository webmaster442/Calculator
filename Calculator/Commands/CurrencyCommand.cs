using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class CurrencyCommand : ShellCommandAsync
{
    public CurrencyCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["currency"];

    public override string Synopsys
        => "Converts between currency exchange rates";

    public override async Task Execute(Arguments args, CancellationToken cancellationToken)
    {
        try
        {
            Dictionary<string, decimal> currencies = await Host.WebServices.GetCurrencyRates();

            if (args.Length == 0)
            {
                PrintTable(currencies);
                return;
            }

            args.ThrowIfNotSpecifiedAtLeast(3);

            decimal ammount = args.Parse<decimal>(0);
            string soruceCurrency = args[1].ToUpper();
            string targetCurrency = args[2].ToUpper();

            if (!currencies.ContainsKey(soruceCurrency))
                throw new CommandException($"Unknown currency: {soruceCurrency}");

            if (!currencies.ContainsKey(targetCurrency))
                throw new CommandException($"Unknown currency: {targetCurrency}");

            decimal baseUnitValue = currencies[soruceCurrency] * ammount;
            decimal resultUnitValue = currencies[targetCurrency] * baseUnitValue;

            Host.Output.Result(resultUnitValue.ToString(Host.CultureInfo));
        }
        catch (Exception ex)
        {
            Host.Output.Error(ex);
        }
    }

    private void PrintTable(Dictionary<string, decimal> currencies)
    {
        var tableData = new TableData("Currency", "Rate");
        foreach (var currency in currencies.OrderBy(x => x.Key)) 
        {
            tableData.AddRow(currency.Key, currency.Value.ToString(Host.CultureInfo));
        }
        Host.Output.Table(tableData);
    }
}
