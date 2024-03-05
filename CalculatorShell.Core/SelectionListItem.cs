//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

public sealed class SelectionListItem
{
    public required string Item { get; init; }
    public required string Description { get; init; }
    public required bool IsChecked { get; init; }
}