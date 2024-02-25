//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

namespace CalculatorShell.Engine.Simplification;

internal static class Utilities
{
    public static void GetBalanced(ref string a, ref string b)
    {
        if (a.Length < b.Length)
            a = a.PadLeft(b.Length, '0');
        else
            b = b.PadLeft(a.Length, '0');
    }

    public static int GetDifferences(string a, string b)
    {
        GetBalanced(ref a, ref b);

        int differences = 0;

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
                differences++;
        }

        return differences;
    }

    public static string GetMask(string a, string b)
    {
        GetBalanced(ref a, ref b);

        StringBuilder final = new(a.Length);

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
                final.Append('-');
            else
                final.AppendFormat("{0}", a[i]);
        }

        return final.ToString();
    }

    public static bool ContainsSubList(List<int> list, HashSet<int> OtherList)
    {
        bool ret = true;
        foreach (int item in OtherList)
        {
            if (!list.Contains(item))
            {
                ret = false;
                break;
            }
        }
        return ret;
    }

    public static bool ContainsAtleastOne(List<int> list, HashSet<int> OtherList)
    {
        bool ret = false;
        foreach (int item in OtherList)
        {
            if (list.Contains(item))
            {
                ret = true;
                break;
            }
        }
        return ret;
    }

    public static string GetBinaryValue(int number, int chars)
    {
        string bin = Convert.ToString(number, 2);
        if (bin.Length < chars)
            return bin.PadLeft(chars, '0');
        return bin;
    }
}
