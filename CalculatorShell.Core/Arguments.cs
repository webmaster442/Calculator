//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections;

namespace CalculatorShell.Core;

public sealed class Arguments : IEnumerable<string>
{
    private readonly IReadOnlyList<string> _arguments;

    public Arguments(IReadOnlyList<string> commandLine)
    {
        _arguments = commandLine;
        Text = string.Join(' ', _arguments);
    }

    public string Text { get; }

    public string this[int index]
        => _arguments[index];

    public int Length => _arguments.Count;

    public IEnumerator<string> GetEnumerator()
        => _arguments.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _arguments.GetEnumerator();
}
