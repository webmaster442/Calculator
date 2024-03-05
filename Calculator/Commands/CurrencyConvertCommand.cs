//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.ArgumentCompleters;
using Calculator.Internal;

using CalculatorShell.Core;

using CommandLine;

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

    public override string HelpMessage
        => this.BuildHelpMessage<CurrencyConvertOptions>();

    public override IArgumentCompleter? ArgumentCompleter
        => new DelegatedCompleter(ProvideAutoCompleteItems);

    internal class CurrencyConvertOptions
    {
        [Value(0, HelpText = "Target currency", Required = true)]
        public decimal Ammount { get; set; }

        [Value(1, HelpText = "Source Currency", Required = true)]
        public string Source { get; set; }

        [Value(2, HelpText = "Target Currency", Required = true)]
        public string Target { get; set; }

        public CurrencyConvertOptions()
        {
            Source = string.Empty;
            Target = string.Empty;
        }
    }

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        Dictionary<string, decimal> currencies = await Host.WebServices.GetCurrencyRates();

        if (args.Length == 0)
        {
            PrintTable(currencies);
            return;
        }

        var options = args.Parse<CurrencyConvertOptions>(Host);
        options.Source = options.Source.ToUpper();
        options.Target = options.Target.ToUpper();

        if (!currencies.TryGetValue(options.Source, out decimal sourceValue))
            throw new CommandException($"Unknown currency: {options.Source}");

        if (!currencies.TryGetValue(options.Target, out decimal targetValue))
            throw new CommandException($"Unknown currency: {options.Target}");

        decimal baseUnitValue = sourceValue * options.Ammount;
        decimal resultUnitValue = targetValue * baseUnitValue;

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
        string currentWord = BaseCompleter.GetWordAtCaret(text, caret, out _);
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
