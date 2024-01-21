using CalculatorShell.Core;

namespace Calculator.ArgumentCompleters;

internal sealed class DirectoryNameCompleter : BaseCompleter
{
    public DirectoryNameCompleter(ILog log) : base(log)
    {
    }

    public override IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret)
    {

        string word = GetWordAtCaret(text, caret);

        try
        {
            var dirs = new DirectoryInfo(Environment.CurrentDirectory)
            .GetDirectories()
            .Select(d => (d.Name, $"Last modified: {d.LastWriteTime}"));

            var filtered = dirs.Where(d => d.Name.StartsWith(word)).ToArray();

            if (filtered.Length > 0)
                return filtered;

            return dirs;
        }
        catch (Exception ex)
        {
            Log.Exception(ex);
            return Enumerable.Empty<(string, string)>();
        }
    }
}
