//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class CurrencyConvertCommand : ShellCommandAsync
{
    private readonly string[] _currencies;

    public CurrencyConvertCommand(IHost host) : base(host)
    {
        _currencies =
        [
            "AUD",
            "BGN",
            "BRL",
            "CAD",
            "CHF",
            "CNY",
            "CZK",
            "DKK",
            "EUR",
            "GBP",
            "HKD",
            "IDR",
            "ILS",
            "INR",
            "ISK",
            "JPY",
            "KRW",
            "MXN",
            "MYR",
            "NOK",
            "NZD",
            "PHP",
            "PLN",
            "RON",
            "RSD",
            "RUB",
            "SEK",
            "SGD",
            "THB",
            "TRY",
            "UAH",
            "USD",
            "ZAR",
            "HUF"
        ];
    }

    public override string[] Names => ["currency-convert"];

    public override string Synopsys
        => "Converts between currency exchange rates";

    public override IArgumentCompleter? ArgumentCompleter
        => new DelegatedCompleter(ProvideAutoCompleteItems);

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
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

    private void PrintTable(Dictionary<string, decimal> currencies)
    {
        var tableData = new TableData("Currency", "Rate");
        foreach (var currency in currencies.OrderBy(x => x.Key))
        {
            tableData.AddRow(currency.Key, currency.Value.ToString(Host.CultureInfo));
        }
        Host.Output.Table(tableData);
    }

    private IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret)
    {
        string currentWord = BaseCompleter.GetWordAtCaret(text, caret);
        if (double.TryParse(currentWord, Host.CultureInfo, out _))
        {
            return Enumerable.Empty<(string option, string description)>();
        }

        var results = _currencies
            .Where(x => x.StartsWith(currentWord, StringComparison.OrdinalIgnoreCase))
            .Order()
            .Select(x => (x, string.Empty));

        if (results.Any())
            return results;

        return _currencies
            .Order()
            .Select(x => (x, string.Empty));
    }
}
