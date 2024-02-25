//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections;

namespace CalculatorShell.Core;

public sealed class TableData : IEnumerable<string[]>
{
    private readonly List<string[]> _rows;
    public string[] Headers { get; }


    public TableData(params string[] headers)
    {
        _rows = new List<string[]>();
        Headers = headers;
    }

    public int Rows
        => _rows.Count - 1;

    public void AddRow(params string[] rowData)
    {
        if (rowData.Length != Headers.Length)
            throw new InvalidOperationException("Data count must match header count");

        _rows.Add(rowData);
    }

    public IEnumerator<string[]> GetEnumerator()
        => _rows.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _rows.GetEnumerator();
}
