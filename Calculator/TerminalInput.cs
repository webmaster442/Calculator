using System.Globalization;

using CalculatorShell.Core;

namespace Calculator;

using LineReader = CalculatorShell.Core.Readline.ReadLine;

internal sealed class TerminalInput : ITerminalInput, CalculatorShell.Core.Readline.IAutoCompleteHandler
{
    public string Prompt { get; set; }
    public CultureInfo CultureInfo { get; set; }
    public char[] Separators { get; set; }

    private string[] _suggestions;

    public TerminalInput()
    {
        _suggestions = Array.Empty<string>();
        Prompt = string.Empty;
        Separators = [' '];
        CultureInfo = CultureInfo.InvariantCulture;
        LineReader.HistoryEnabled = true;
        LineReader.HistorySize = 100;
        LineReader.UndoEnabled = true;
        LineReader.AutoCompletionEnabled = true;
        LineReader.AutoCompletionHandler = this;
    }

    public (string cmd, Arguments Arguments) ReadLine()
    {
        string line = LineReader.Read(Prompt);
        return ArgumentsFactory.Create(line, CultureInfo);
    }

    public string[] GetSuggestions(string text, int index)
    {
        string key = text[index..];
        return _suggestions.Where(x => x.StartsWith(key, StringComparison.OrdinalIgnoreCase)).ToArray();
    }

    internal void SetCommands(IEnumerable<string> keys, HashSet<string> exitCommands)
    {
        _suggestions = keys.Concat(exitCommands).Order().ToArray();
    }
}