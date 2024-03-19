﻿//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Reflection;

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.ArgumentCompleters;

internal class OptionClassCompleter<TOptions> : BaseCompleter
{
    private readonly Dictionary<string, string> _options;

    public OptionClassCompleter(IHost host) : base(host)
    {
        _options = [];
        Fill();
    }

    private void Fill()
    {
        foreach (var prop in typeof(TOptions).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var option = prop.GetCustomAttribute<OptionAttribute>();
            if (option != null)
            {
                _options.Add($"-{option.ShortName}", option.HelpText);
                _options.Add($"--{option.LongName}", option.HelpText);
            }
        }
    }

    public override IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret)
    {
        var candidates = _options
            .Where(o => o.Key.StartsWith(GetWordAtCaret(text, caret, out _)))
            .Select(o => (o.Key, o.Value))
            .ToArray();

        if (candidates.Length > 0)
            return candidates;

        return _options.Select(o => (o.Key, o.Value));

    }
}
