using CalculatorShell.Core;

namespace Calculator.ArgumentCompleters;

internal sealed class FileNameCompleter : BaseCompleter
{
    public FileNameCompleter(ILog log) : base(log)
    {
    }

    public override IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret)
    {
        string word = GetWordAtCaret(text, caret);

        try
        {
            var files = new DirectoryInfo(Environment.CurrentDirectory)
                .GetFiles()
                .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden))
                .Select(f => (f.Name, $"Size: {f.Length}"));

            var filtered = files.Where(f => f.Name.StartsWith(word)).ToArray();

            if (filtered.Length > 0)
                return filtered;

            return files;
        }
        catch (Exception ex)
        {
            Log.Exception(ex);
            return Enumerable.Empty<(string option, string description)>();
        }
    }
}
