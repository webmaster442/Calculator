using System.Text;

namespace CalculatorShell.Core;

public static class ArgumentsFactory
{
    public static List<string> Tokenize(string input)
    {
        List<string> items = new();
        StringBuilder temp = new();
        bool isQutes = false;

        if (string.IsNullOrEmpty(input))
            return items;

        foreach (var chr in input)
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
        return items;
    }

    public static (string cmd, Arguments args) Create(string line, IFormatProvider formatProvider)
    {
        if (string.IsNullOrEmpty(line))
            return (string.Empty, new Arguments(Array.Empty<string>(), formatProvider));

        var tokens = Tokenize(line);

        return (tokens[0], new Arguments(tokens.Skip(1).ToArray(), formatProvider));
    }
}
