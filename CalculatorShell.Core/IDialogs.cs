//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

public interface IDialogs
{
    Task<string> SelectFile(CancellationToken cancellationToken);
    Task<string> SelectDirectory(CancellationToken cancellationToken);
    Task<SelectionListItem> SelectionList(string title, IEnumerable<SelectionListItem> items, CancellationToken cancellationToken);
    Task<IReadOnlyList<SelectionListItem>> MultiSelectionList(string title, IEnumerable<SelectionListItem> items, CancellationToken cancellationToken);
    void OpenServerDocument(ServerDocument document);
}
