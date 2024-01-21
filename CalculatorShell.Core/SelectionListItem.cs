
namespace CalculatorShell.Core;

public sealed class SelectionListItem
{
    public required string Item { get; init; }
    public required string Description { get; init; }
    public required bool IsChecked { get; init; }
}