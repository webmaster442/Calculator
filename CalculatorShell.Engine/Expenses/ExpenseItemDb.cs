using System.Diagnostics;

namespace CalculatorShell.Engine.Expenses;

public sealed class ExpenseItemDb
{
    private readonly string _folder;
    private readonly List<ExpenseItem> _cache;
    private bool _wasEdited;

    public string DatabaseFile { get; private set; }

    public ExpenseItemDb(string folder)
    {
        _folder = folder;

        if (!Directory.Exists(_folder))
            Directory.CreateDirectory(_folder);

        _cache = new List<ExpenseItem>();
        DatabaseFile = Path.Combine(_folder, $"{DateTime.Now.Year}-{DateTime.Now.Month}.csv");
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

                var item = line.ToExpenseItem();
                if (item != null)
                    _cache.Add(item);
            }
        }
    }

    public void Insert(ExpenseItem item)
    {
        if (File.Exists(DatabaseFile))
        {
            File.AppendAllLines(DatabaseFile, new[] { item.ToCsv() });
        }
        else
        {
            File.AppendAllLines(DatabaseFile, new[]
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

    public IEnumerable<ExpenseItem> Items
    {
        get
        {
            if (_wasEdited)
            {
                _cache.Clear();
                Load();
                _wasEdited = false;
            }
            return _cache;
        }
    }
}
