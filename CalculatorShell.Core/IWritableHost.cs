//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

namespace CalculatorShell.Core;

public interface IWritableHost
{
    void SetCommandData(IReadOnlyDictionary<string, string> commandHelps,
                        IReadOnlyDictionary<string, IArgumentCompleter> completers,
                        ISet<string> exitCommands);

    string CurrentDirectory { get; set; }
    CultureInfo CultureInfo { get; set; }
}
