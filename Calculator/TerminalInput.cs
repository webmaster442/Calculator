﻿using System.Globalization;

using CalculatorShell.Core;


namespace Calculator;

internal sealed class TerminalInput : ITerminalInput
{
    public string Prompt
    {
        get => _configuration.Prompt.ToString() ?? string.Empty;
        set => _configuration.Prompt = value;
    }
    public CultureInfo CultureInfo { get; set; }
    public char[] Separators { get; set; }

    private readonly PrettyPrompt.Prompt _reader;
    private readonly PrettyPrompt.PromptConfiguration _configuration;
    private readonly Callbacks _callbacks;

    public TerminalInput()
    {
        _configuration = new PrettyPrompt.PromptConfiguration();
        _callbacks = new Callbacks();
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

    internal void SetCommands(IEnumerable<string> keys, HashSet<string> exitCommands)
    {
        _callbacks.Data = keys.Concat(exitCommands).ToArray();
    }
}