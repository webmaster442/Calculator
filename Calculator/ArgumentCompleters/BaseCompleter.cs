﻿using Calculator.Internal;
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

    protected Options Options
        => Host.Mediator.Request<Options, OptionsRequest>(new OptionsRequest()) 
            ?? throw new InvalidOperationException("Options shoudln't be null");

    protected bool FilterHiddenBasedOnOptons(FileSystemInfo info)
    {
        if (Options.ShowHiddenFiles)
            return true;

        return !info.Attributes.HasFlag(FileAttributes.Hidden);
    }

    public static string GetWordAtCaret(string text, int caret)
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
