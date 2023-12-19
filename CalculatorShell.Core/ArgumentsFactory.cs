using System.Text;

namespace CalculatorShell.Core;

public static class ArgumentsFactory
{
    public static (string cmd, Arguments args) Create(string line, IFormatProvider formatProvider)
    {
        List<string> items = new();
        StringBuilder temp = new();
        bool isQutes = false;

        foreach (var chr in line)
        {
            if (chr == ' ')
            {
                if (isQutes)
                {
                    temp.Append(chr);
                }
                else
                {
                    items.Add(temp.ToString());
                    temp.Clear();
                }
            }
            else if (chr == '"')
            {
                isQutes = !isQutes;
            }
            else
            {
                temp.Append(chr);
            }
        }

        if (temp.Length > 0)
        {
            items.Add(temp.ToString());
        }

        return (items[0], new Arguments(items.Skip(1).ToArray(), formatProvider));
    }
}
