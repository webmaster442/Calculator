//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Represents an item, that can be selected via SelectionList or MultiSelectionList
/// <see cref="IDialogs.SelectionList(string, IEnumerable{CalculatorShell.Core.SelectionListItem}, CancellationToken)"/>
/// <see cref="IDialogs.MultiSelectionList(string, IEnumerable{CalculatorShell.Core.SelectionListItem}, CancellationToken)"/>
/// </summary>
public sealed class SelectionListItem
{
    /// <summary>
    /// Item value
    /// </summary>
    public required string Item { get; init; }
    /// <summary>
    /// Item description
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// Item chekced state. Ignored in SelectionList
    /// </summary>
    public required bool IsChecked { get; init; }
}