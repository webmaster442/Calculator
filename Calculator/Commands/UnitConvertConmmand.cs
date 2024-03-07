//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;

using CommandLine;

using UnitsNet;

namespace Calculator.Commands;

internal sealed class UnitConvertConmmand : ShellCommand
{
    private readonly Dictionary<string, HashSet<string>> _knownConversions;

    public UnitConvertConmmand(IHost host) : base(host)
    {
        _knownConversions = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        foreach (var info in Quantity.Infos)
        {
            var names = info.UnitInfos.Select(x => x.Name);
            _knownConversions.Add(info.Name, names.ToHashSet());
        }
    }

    public override string[] Names => ["unit-convert"];

    public override string Category
        => CommandCategories.Conversions;

    public override string Synopsys
        => "Converts a value given in a unit to an other unit";

    public override string HelpMessage
        => this.BuildHelpMessage<UnitConvertOptions>();

    public override IArgumentCompleter? ArgumentCompleter
        => new DelegatedCompleter(ProvideAutoCompleteItems);

    private double Convert(double value, string fromUnit, string toUnit)
    {
        string? quantityName = _knownConversions
            .Where(x => x.Value.Contains(fromUnit)
                     && x.Value.Contains(toUnit))
            .Select(x => x.Key)
            .FirstOrDefault();

        if (string.IsNullOrEmpty(quantityName))
            throw new InvalidOperationException($"Can't convert from {fromUnit} to {toUnit}");

        return UnitConverter.ConvertByName(value, quantityName, fromUnit, toUnit);
    }

    internal class UnitConvertOptions
    {
        [Value(0, HelpText = "Value to convert", Required = true)]
        public double Value { get; set; }

        [Value(1, HelpText = "Source unit", Required = true)]
        public string Source { get; set; }

        [Value(2, HelpText = "Target unit", Required = true)]
        public string Target { get; set; }

        public UnitConvertOptions()
        {
            Source = string.Empty;
            Target = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<UnitConvertOptions>(Host);

        double result = Convert(options.Value, options.Source, options.Target);

        Host.Output.Result(result.ToString(Host.CultureInfo));
    }

    private IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret)
    {
        string currentWord = BaseCompleter.GetWordAtCaret(text, caret, out _);
        if (double.TryParse(currentWord, Host.CultureInfo, out _))
        {
            return Enumerable.Empty<(string option, string description)>();
        }

        var results = _knownConversions
            .SelectMany(x => x.Value)
            .Where(x => x.StartsWith(currentWord, StringComparison.OrdinalIgnoreCase))
            .Order()
            .Select(x => (x, string.Empty));

        if (results.Any())
            return results;

        return _knownConversions
            .SelectMany(x => x.Value)
            .Select(x => (x, string.Empty));
    }
}
