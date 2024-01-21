using CalculatorShell.Core;

namespace Calculator.ArgumentCompleters;

internal abstract class BaseCompleter : IArgumentCompleter
{
    protected BaseCompleter(IHost host)
    {
        Host = host;
    }

    protected IHost Host { get; }

    protected static string GetWordAtCaret(string text, int caret)
    {
        var words = ArgumentsFactory.Tokenize(text);
        string wordAtCaret = string.Empty;
        int currentIndex = 0;
        foreach (var word in words)
        {
            if (currentIndex < caret && caret <= currentIndex + word.Length)
            {
                wordAtCaret = word;
                break;
            }
            currentIndex += word.Length + 1; // +1 due to word separator
        }

        return wordAtCaret;
    }

    public abstract IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret);
}
