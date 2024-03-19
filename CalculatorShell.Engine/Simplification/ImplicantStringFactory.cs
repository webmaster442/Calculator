//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

namespace CalculatorShell.Engine.Simplification;

internal static class ImplicantStringFactory
{
    public static string Create(Implicant implicant, int length, string[] variables)
    {
        string? mask = implicant.Mask.PadLeft(length, '0');
        string result = CreateMsb(mask, variables);

        if (!string.IsNullOrWhiteSpace(result))
            return $"({result[0..^1]})";

        return result;
    }

    private static string CreateMsb(string mask, string[] variables)
    {
        StringBuilder builder = new();
        for (int i = 0; i < mask.Length; i++)
        {

            if (mask[i] == '0') _ = builder.AppendFormat("!{0} &", variables[i]);
            else if (mask[i] == '1') _ = builder.AppendFormat("{0} &", variables[i]);

        }
        return builder.ToString();
    }
}
