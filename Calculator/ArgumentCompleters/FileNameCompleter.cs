//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator.ArgumentCompleters;

internal sealed class FileNameCompleter : BaseCompleter
{
    public FileNameCompleter(IHost host) : base(host)
    {
    }

    public override IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret)
    {
        string word = GetWordAtCaret(text, caret, out _);

        try
        {
            var files = new DirectoryInfo(Host.CurrentDirectory)
                .GetFiles()
                .Where(FilterHiddenBasedOnOptons)
                .Select(f => (f.Name, $"Size: {f.Length}"));

            var filtered = files.Where(f => f.Name.StartsWith(word)).ToArray();

            if (filtered.Length > 0)
                return filtered;

            return files;
        }
        catch (Exception ex)
        {
            Host.Log.Exception(ex);
            return Enumerable.Empty<(string option, string description)>();
        }
    }
}
