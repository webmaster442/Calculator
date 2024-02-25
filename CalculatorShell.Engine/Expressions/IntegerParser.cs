//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace CalculatorShell.Engine.Expressions;

internal static partial class IntegerParser
{
    [GeneratedRegex("^[0-9_]+", RegexOptions.None, 1000)]
    private static partial Regex Decimal();

    [GeneratedRegex("^(Hx)([0-9_a-zA-Z])+", RegexOptions.None, 1000)]
    private static partial Regex Hexa();

    [GeneratedRegex("^(Ox)([0-7])+", RegexOptions.None, 1000)]
    private static partial Regex Octa();

    [GeneratedRegex("^(Bx)(0|1)+", RegexOptions.None, 1000)]
    private static partial Regex Binary();

    private static Dictionary<char, int> Symbols = new()
    {
        { '0', 0 },
        { '1', 1 },
        { '2', 2 },
        { '3', 3 },
        { '4', 4 },
        { '5', 5 },
        { '6', 6 },
        { '7', 7 },
        { '8', 8 },
        { '9', 9 },
        { 'a', 10 },
        { 'b', 11 },
        { 'c', 12 },
        { 'd', 13 },
        { 'e', 14 },
        { 'f', 15 },
    };

    //decimal: 0123456789_
    //hex: starts with Hx or hx 01234556789_ABCDEFabcdef
    //oct: starts with Ox or ox 01234567_
    //bin: starts with Bx or bx 01_
    public static bool TryParse(string? str, IFormatProvider? formatProvider, out Int128 parsed)
    {
        if (str == null)
        {
            parsed = default;
            return false;
        }

        try
        {
            if (Decimal().IsMatch(str))
            {
                return Int128.TryParse(str.Replace("_", ""), formatProvider, out parsed);
            }
            else if (Hexa().IsMatch(str))
            {
                parsed = ParseFromBase(str[2..], 16);
                return true;
            }
            else if (Octa().IsMatch(str))
            {
                parsed = ParseFromBase(str[2..], 8);
                return true;
            }
            else if (Binary().IsMatch(str))
            {
                parsed = ParseFromBase(str[2..], 2);
                return true;
            }
            else
            {
                parsed = default;
                return false;
            }
        }
        catch (Exception)
        {
            parsed = default;
            return false;
        }
    }

    private static Int128 ParseFromBase(string text, int @base)
    {
        string preprocessed = text.Replace("_", "").ToLower();
        Int128 exponent = 1;
        Int128 result = 0;
        for (int i = preprocessed.Length - 1; i >= 0; i--)
        {
            int multiplier = Symbols[preprocessed[i]];
            result += multiplier * exponent;
            exponent *= @base;
        }
        return result;
    }
}
