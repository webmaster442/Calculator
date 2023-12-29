using System.Collections;
using System.Diagnostics;

namespace CalculatorShell.Expenses;

public sealed class ExpenseItemDb : IEnumerable<ExpenseItem>
{
    private readonly string _folder;
    private readonly List<ExpenseItem> _cache;
    private bool _wasEdited;

    public string DatabaseFile { get; private set; }

    public ExpenseItemDb(string folder)
    {
        _folder = folder;
        _cache = new List<ExpenseItem>();
        DatabaseFile = Path.Combine(_folder, $"{DateTime.Now.Year}-{DateTime.Now.Year}.csv");
        Load();
        _wasEdited = false;
    }

    public void SetDatabase(int year, int month)
    {
        DatabaseFile = Path.Combine(_folder, $"{year}-{month}.csv");
        _cache.Clear();
        Load();
    }

    public void Load()
    {
        if (!File.Exists(DatabaseFile))
            return;

        using (var reader = File.OpenText(DatabaseFile))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                _cache.Add(line.ToExpenseItem());
            }
        }
    }

    public async Task Insert(ExpenseItem item)
    {
        if (File.Exists(DatabaseFile))
        {
            await File.AppendAllLinesAsync(DatabaseFile, new[] { item.ToCsv() });
        }
        else
        {
            await File.AppendAllLinesAsync(DatabaseFile, new[]
            {
                item.ToCsvHeader(),
                item.ToCsv()
            });
        }
        _cache.Add(item);
    }

    public void Edit()
    {
        _wasEdited = true;

        if (!File.Exists(DatabaseFile))
            throw new InvalidOperationException($"{DatabaseFile} doesn't exist");

        using (var p = new Process())
        {
            p.StartInfo.FileName = DatabaseFile;
            p.StartInfo.UseShellExecute = true;
            p.Start();
        }
    }

    public IEnumerator<ExpenseItem> GetEnumerator()
    {
        if (_wasEdited)
        {
            _cache.Clear();
            Load();
            _wasEdited = false;
        }
        return _cache.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
