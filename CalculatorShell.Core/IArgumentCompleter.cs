//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

public interface IArgumentCompleter
{
    IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret);
}