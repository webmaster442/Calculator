//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

public interface IHelpDataSetter
{
    void SetCommandData(IReadOnlyDictionary<string, string> commandHelps,
                        IReadOnlyDictionary<string, IArgumentCompleter> completers,
                        ISet<string> exitCommands);
}
