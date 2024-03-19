//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Configuration;
using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.ArgumentCompleters;

internal abstract class BaseCompleter : IArgumentCompleter
{
    protected BaseCompleter(IHost host)
    {
        Host = host;
    }

    protected IHost Host { get; }

    protected Config Config
        => Host.Mediator.Request<Config, ConfigRequest>(new ConfigRequest())
            ?? throw new InvalidOperationException("Configuration shoudln't be null");

    protected bool FilterHiddenBasedOnOptons(FileSystemInfo info)
    {
        if (Config.ShowHiddenFiles)
            return true;

        return !info.Attributes.HasFlag(FileAttributes.Hidden);
    }

    public static string GetWordAtCaret(string text, int caret, out int wordIndex)
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
        wordIndex = currentIndex;
        return wordAtCaret;
    }

    public abstract IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret);
}
