//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace CalculatorShell.Core;

public sealed class Arguments
{
    private readonly IReadOnlyList<string> _arguments;
    private readonly IFormatProvider _formatProvider;

    public Arguments(IReadOnlyList<string> commandLine, IFormatProvider formatProvider)
    {
        _arguments = commandLine;
        _formatProvider = formatProvider;
        Text = string.Join(' ', _arguments);
    }

    public string Text { get; }

    public string this[int index]
        => _arguments[index];

    public string? this[string shortName, string longName]
        => FindArgument(shortName, longName);

    private string? FindArgument(string shortName, string longName)
    {
        for (int i = 0; i < _arguments.Count; i++)
        {
            string current = _arguments[i];
            string next = i + 1 < _arguments.Count ? _arguments[i + 1] : string.Empty;
            if ((current == shortName || current == longName)
                && !string.IsNullOrEmpty(next))
            {
                return next;
            }
        }
        return null;
    }

    public int IndexOf(string shortName, string longName)
    {
        for (int i = 0; i < _arguments.Count; i++)
        {
            string current = _arguments[i];
            if (current == shortName || current == longName)
            {
                return i;
            }
        }
        return -1;
    }

    public int Length => _arguments.Count;

    public T Parse<T>(int index) where T : IParsable<T>
    {
        return T.Parse(_arguments[index], _formatProvider);
    }

    public T Parse<T>(int index, bool ignoreCase = true) where T: struct, Enum
    {
        return Enum.Parse<T>(_arguments[index], ignoreCase);
    }

    public T Parse<T>(string shortName, string longName) where T : IParsable<T>
    {
        var found = FindArgument(shortName, longName)
            ?? throw new CommandException($"Argument {shortName} or {longName} was not specified");

        return T.Parse(found, _formatProvider);
    }

    public bool TryParse<T>(string shortName, string longName, [MaybeNullWhen(false)] out T? result) where T : IParsable<T>
    {
        try
        {
            result = Parse<T>(shortName, longName);
            return true;
        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }

    public IEnumerable<string> AsEnumerable() => _arguments;

    public void ThrowIfNotSpecifiedAtLeast(int number)
    {
        if (_arguments.Count < number)
            throw new ComandException($"Command expected at least {number} arguments. Got: {_arguments.Count}");
    }
}
