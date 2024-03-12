//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections;

namespace CalculatorShell.Core;

/// <summary>
/// A Class representing shell command arguments
/// </summary>
public sealed class Arguments : IReadOnlyList<string>
{
    private readonly IReadOnlyList<string> _arguments;

    /// <summary>
    /// Creates a new instance of Arguments
    /// </summary>
    /// <param name="commandLine">command line argument parts</param>
    public Arguments(IReadOnlyList<string> commandLine)
    {
        _arguments = commandLine;
        Text = string.Join(' ', _arguments);
    }

    /// <summary>
    /// Arguments as text, as they were entered, without quotes
    /// </summary>
    public string Text { get; }

    /// <inheritdoc/>
    public string this[int index]
        => _arguments[index];

    /// <inheritdoc/>
    public int Count => _arguments.Count;

    /// <inheritdoc/>
    public IEnumerator<string> GetEnumerator()
        => _arguments.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _arguments.GetEnumerator();
}
