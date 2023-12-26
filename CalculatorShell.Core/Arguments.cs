using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace CalculatorShell.Core;

public sealed class Arguments
{
    private readonly IReadOnlyList<string> _arguments;
    private readonly IFormatProvider _formatProvider;

    public Arguments(IReadOnlyList<string> commandLine, IFormatProvider formatProvider)
    {
        _arguments = commandLine;
        _formatProvider = formatProvider;
    }

    public string this[int index]
        => _arguments[index];

    public int Length => _arguments.Count;

    public T Parse<T>(int index) where T : IParsable<T>
    {
        return T.Parse(_arguments[index], _formatProvider);
    }

    public bool TryParseAll<T>(out T?[] parsed) where T : IParsable<T>
    {
        parsed = new T[_arguments.Count];
        for (int i = 0; i < _arguments.Count; i++)
        {
            if (!TryParse<T>(i, out T? temp))
            {
                return false;
            }
            else
            {
                parsed[i] = temp;
            }
        }
        return true;
    }

    public bool TryParse<T>(int index, [MaybeNullWhen(false)] out T? parsed) where T : IParsable<T>
    {
        return T.TryParse(_arguments[index], _formatProvider, out parsed);
    }

    public IEnumerable<string> AsEnumerable() => _arguments;

    public void ThrowIfNotSpecifiedAtLeast(int number)
    {
        if (_arguments.Count < number)
            throw new ComandException($"Command expected at least {number} arguments. Got: {_arguments.Count}");
    }
}
