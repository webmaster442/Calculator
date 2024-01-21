namespace Calculator.ArgumentCompleters;

internal sealed class DirectoryNameCompleter : BaseCompleter
{
    public override IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret)
    {
        string word = GetWordAtCaret(text, caret);

        var dirs = new DirectoryInfo(Environment.CurrentDirectory)
            .GetDirectories()
            .Where(d => d.Name.StartsWith(word))
            .Select(d => (d.Name, $"Last modified: {d.LastWriteTime}"));

        return dirs;
    }
}
