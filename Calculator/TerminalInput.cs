using System.Globalization;

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

    internal void SetCommandData(IReadOnlyDictionary<string, string> commandHelps, HashSet<string> exitCommands)
    {
        var dict = commandHelps.ToDictionary();
        foreach (var cmd in exitCommands) 
        {
            dict.Add(cmd, "Exit program");
        }
        _callbacks.Data = dict;
    }
}