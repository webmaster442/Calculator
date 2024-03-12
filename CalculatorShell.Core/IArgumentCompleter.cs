//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Argument completer interface
/// </summary>
public interface IArgumentCompleter
{
    /// <summary>
    /// Provides autocomplete items
    /// </summary>
    /// <param name="text">Raw user input text</param>
    /// <param name="caret">Caret position in the texrt</param>
    /// <returns>a collection of completion items</returns>
    IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret);
}