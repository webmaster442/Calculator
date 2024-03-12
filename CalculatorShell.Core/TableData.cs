//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections;

namespace CalculatorShell.Core;

/// <summary>
/// Represents data that needs to be displayed in table format
/// </summary>
public sealed class TableData : IEnumerable<string[]>
{
    private readonly List<string[]> _rows;

    /// <summary>
    /// Table headers
    /// </summary>
    public string[] Headers { get; }

    /// <summary>
    /// Create a new instance of TableData
    /// </summary>
    /// <param name="headers">Table headers</param>
    public TableData(params string[] headers)
    {
        _rows = new List<string[]>();
        Headers = headers;
    }

    /// <summary>
    /// Number of rows in the table
    /// </summary>
    public int Rows
        => _rows.Count - 1;

    /// <summary>
    /// Add a new row to the table
    /// </summary>
    /// <param name="columns">Columns in the row</param>
    /// <exception cref="InvalidOperationException">When the columns count doesn't match the header count</exception>
    public void AddRow(params string[] columns)
    {
        if (columns.Length != Headers.Length)
            throw new InvalidOperationException("Data count must match header count");

        _rows.Add(columns);
    }

    /// <inheritdoc/>
    public IEnumerator<string[]> GetEnumerator()
        => _rows.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _rows.GetEnumerator();
}
