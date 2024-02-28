//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Reflection;

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

namespace Calculator.Commands;
internal class FileSizeCommand : ShellCommand
{
    private readonly Dictionary<string, long> _units;

    public FileSizeCommand(IHost host) : base(host)
    {
        _units = typeof(FileSizeCalculator)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .ToDictionary(f => f.Name, f => (long)f.GetValue(null)!, StringComparer.InvariantCultureIgnoreCase);
    }

    public override IArgumentCompleter? ArgumentCompleter
        => new DelegatedCompleter(ProvideAutoCompleteItems);

    public override string[] Names
        => ["filesize"];

    public override string Synopsys
        => "Converts file sizes to human readable form";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(1);

        double value = args.Parse<double>(0);
        string unit = args.Length == 1 ? "bytes" : args[1];

        long bytes = FileSizeCalculator.ToBytes(value, unit);

        string result = FileSizeCalculator.ToHumanReadable(bytes, Host.CultureInfo);

        Host.Output.Result(result);
    }

    private IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret)
    {
        string currentWord = BaseCompleter.GetWordAtCaret(text, caret, out _);

        if (double.TryParse(currentWord, Host.CultureInfo, out _))
        {
            return Enumerable.Empty<(string option, string description)>();
        }

        var results = _units
            .Where(x => x.Key.StartsWith(currentWord, StringComparison.OrdinalIgnoreCase))
            .OrderBy(x => x.Key)
            .Select(x => (x.Key, $"{x.Value} bytes"));

        if (results.Any())
            return results;

        return _units
            .OrderBy(x => x.Key)
            .Select(x => (x.Key, $"{x.Value} bytes"));
    }
}
