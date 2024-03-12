//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// User interaction dialogs
/// </summary>
public interface IDialogs
{
    /// <summary>
    /// Displays a file selection dialog.
    /// </summary>
    /// <param name="cancellationToken">A cancellationToken</param>
    /// <returns>Path of selected file</returns>
    Task<string> SelectFile(CancellationToken cancellationToken);
    /// <summary>
    /// Displays a directory selection dialog.
    /// </summary>
    /// <param name="cancellationToken">A cancellationToken</param>
    /// <returns>Path of selected file</returns>
    Task<string> SelectDirectory(CancellationToken cancellationToken);
    /// <summary>
    /// Displays a signle item selection dialog.
    /// </summary>
    /// <param name="title">Selection dialog title</param>
    /// <param name="items">Items to allow chosing from</param>
    /// <param name="cancellationToken">A cancellationToken</param>
    /// <returns>The selected item</returns>
    Task<SelectionListItem> SelectionList(string title, IEnumerable<SelectionListItem> items, CancellationToken cancellationToken);
    /// <summary>
    /// Displays a muti selection dialog, where the user is able to select multiple items
    /// </summary>
    /// <param name="title">Selection dialog title</param>
    /// <param name="items">Items to allow chosing from</param>
    /// <param name="cancellationToken">A cancellationToken</param>
    /// <returns>The selected items</returns>
    Task<IReadOnlyList<SelectionListItem>> MultiSelectionList(string title, IEnumerable<SelectionListItem> items, CancellationToken cancellationToken);
    /// <summary>
    /// Open a web browser to display a document on the running server
    /// </summary>
    /// <param name="document">Document to open in the browser</param>
    void OpenServerDocument(ServerDocument document);
}
