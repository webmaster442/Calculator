namespace CalculatorShell.Core;

public interface IDialogs
{
    Task<string> SelectFile(CancellationToken cancellationToken);
    Task<string> SelectDirectory(CancellationToken cancellationToken);
    Task<IReadOnlyList<SelectionListItem>> SelectionList(string title, IEnumerable<SelectionListItem> items, CancellationToken cancellationToken);
}