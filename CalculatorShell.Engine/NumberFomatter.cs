//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics;
using System.Globalization;
using System.Numerics;

namespace CalculatorShell.Engine;

public static class NumberFomatter
{
    public static string ToString(long number, CultureInfo cultureInfo, bool thousands)
        => Format(number.ToString(cultureInfo), cultureInfo, thousands);

    public static string ToString(double number, CultureInfo cultureInfo, bool thousands)
        => Format(number.ToString(cultureInfo), cultureInfo, thousands);

    public static string ToString(decimal number, CultureInfo cultureInfo, bool thousands)
        => Format(number.ToString(cultureInfo), cultureInfo, thousands);

    public static string ToString(Complex complex, CultureInfo cultureInfo, bool thousands)
    {
        string real = Format(complex.Real.ToString(cultureInfo), cultureInfo, thousands);
        string imaginary = Format(complex.Imaginary.ToString(cultureInfo), cultureInfo, thousands);
        return $"real: {real} imaginary: {imaginary}";
    }

    public static string ToString(Number number, CultureInfo cultureInfo, bool thousands)
    {
        return number.NumberType switch
        {
            NumberType.Double or NumberType.Integer => Format(number.ToString(cultureInfo), cultureInfo, thousands),
            NumberType.Complex => ToString(number.ToComplex(), cultureInfo, thousands),
            NumberType.Fraction => Format(number.ToFraction(), cultureInfo, thousands),
            _ => throw new UnreachableException(),
        };
    }

    private static string Format(Fraction fraction, CultureInfo cultureInfo, bool thousands)
    {
        string line1 = Format(fraction.Numerator.ToString(cultureInfo), cultureInfo, thousands);
        string line2 = Format(fraction.Denominator.ToString(cultureInfo), cultureInfo, thousands);
        string seperator = "-".PadLeft(line1.Length);
        return $"{line1}\r\n{seperator}\r\n{line2}";
    }

    private static string Format(string textForm, CultureInfo cultureInfo, bool thousands)
    {
        if (!thousands)
            return textForm;

        bool isScientific = textForm.Contains('E');
        if (isScientific)
            return textForm;

        Stack<char> result = new(textForm.Length * 2);
        int count = 0;
        bool decimalSplitterFound = false;

        bool containsDecimal = textForm.Contains(cultureInfo.NumberFormat.NumberDecimalSeparator);

        foreach (var chr in textForm.Reverse()) 
        {
            if (chr == cultureInfo.NumberFormat.NumberDecimalSeparator[0])
            {
                result.Push(chr);
                decimalSplitterFound = true;
            }
            else if (decimalSplitterFound || !containsDecimal)
            {
                if (count == 3)
                {
                    result.Push(cultureInfo.NumberFormat.NumberGroupSeparator[0]);
                    result.Push(chr);
                    count = 1;
                }
                else
                {
                    result.Push(chr);
                    ++count;
                }
            }
            else
            {
                result.Push(chr);
            }
        }
        return string.Join("", result);
    }
}
