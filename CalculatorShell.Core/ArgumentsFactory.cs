//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

namespace CalculatorShell.Core;

/// <summary>
/// Argument factory, allows parsing of input to arguments
/// </summary>
public static class ArgumentsFactory
{
    /// <summary>
    /// Tokenize a string by splitting it by space. Items in quotes are not splitted.
    /// </summary>
    /// <param name="input">Input string to tokenize</param>
    /// <returns>Tokenized strings in an array</returns>
    public static IReadOnlyList<string> Tokenize(string input)
    {
        List<string> items = [];
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
                    _ = temp.Append(chr);
                }
                else
                {
                    items.Add(temp.ToString());
                    _ = temp.Clear();
                }
            }
            else if (chr == '"')
            {
                isQutes = !isQutes;
            }
            else
            {
                _ = temp.Append(chr);
            }
        }

        if (temp.Length > 0)
        {
            items.Add(temp.ToString());
        }
        return items;
    }

    /// <summary>
    /// Create command and arguments from an input string 
    /// </summary>
    /// <param name="line">input string</param>
    /// <returns>command name and arguments</returns>
    public static (string cmd, Arguments args) Create(string line)
    {
        if (string.IsNullOrEmpty(line))
            return (string.Empty, new Arguments(Array.Empty<string>()));

        var tokens = Tokenize(line);

        return (tokens[0], new Arguments(tokens.Skip(1).ToArray()));
    }
}
