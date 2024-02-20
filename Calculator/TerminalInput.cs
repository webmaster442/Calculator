﻿using System.Globalization;

using CalculatorShell.Core;

using PrettyPrompt.Highlighting;


namespace Calculator;

internal sealed class TerminalInput : ITerminalInput
{
    public object Prompt
    {
        get => _configuration.Prompt;
        set
        {
            if (value is FormattedString formatted)
                _configuration.Prompt = formatted;
        }
    }

    public CultureInfo CultureInfo { get; }

    public char[] Separators { get; set; }

    private readonly PrettyPrompt.Prompt _reader;
    private readonly PrettyPrompt.PromptConfiguration _configuration;
    private readonly InputPromptCallbacks _callbacks;

    public TerminalInput()
    {
        _configuration = new PrettyPrompt.PromptConfiguration();
        _callbacks = new InputPromptCallbacks();
        _reader = new PrettyPrompt.Prompt(null, _callbacks, null, _configuration);

        Prompt = string.Empty;
        Separators = [' '];
        CultureInfo = CultureInfo.InvariantCulture;
    }

    public (string cmd, Arguments Arguments) ReadLine()
    {
        var result = _reader.ReadLineAsync().GetAwaiter().GetResult();
        return ArgumentsFactory.Create(result.Text, CultureInfo);
    }

    internal void SetCommandData(IReadOnlyDictionary<string, string> commandHelps,
                                 IReadOnlyDictionary<string, IArgumentCompleter> completers,
                                 ISet<string> exitCommands)
    {
        var dict = commandHelps.ToDictionary();
        foreach (var cmd in exitCommands)
        {
            dict.Add(cmd, "Exit program");
        }
        _callbacks.CommandsWithDescription = dict;
        _callbacks.AutoCompletableCommands = completers;
    }
}