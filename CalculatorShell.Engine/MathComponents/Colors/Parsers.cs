using System.Numerics;
using System.Text.RegularExpressions;

namespace CalculatorShell.Engine.MathComponents.Colors;
internal static partial class Parsers
{
    private static readonly char[] FunctionSeperators = ['(', ')', ','];

    private static T[] ParseFunctionForm<T>(string input,
                                            string functionName,
                                            IFormatProvider? provider,
                                            int paramCount = 3) where T : INumberBase<T>
    {
        var parts = input.ToLowerInvariant().Split(FunctionSeperators, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == paramCount + 1
            && parts[0] == functionName)
        {
            T[] parsed = new T[paramCount];
            for (int i = 0; i < paramCount; i++)
            {
                parsed[i] = T.Parse(parts[i + 1], provider);
            }
            return parsed;
        }
        throw new FormatException();
    }

    public static RGB ParseRGB(string s, IFormatProvider? provider)
    {
        if (s.StartsWith('#'))
        {
            byte[] parsed = Convert.FromHexString(s[1..]);
            return new RGB
            {
                R = parsed[0],
                G = parsed[1],
                B = parsed[2],
            };
        }
        else
        {
            int[] rgb = ParseFunctionForm<int>(s, "rgb", provider);
            return new RGB
            {
                R = rgb[0],
                G = rgb[1],
                B = rgb[2],
            };
        }
    }

    public static YUV ParseYUV(string s, IFormatProvider? provider)
    {
        double[] yuv = ParseFunctionForm<double>(s, "yuv", provider);
        return new YUV
        {
            Y = yuv[0],
            U = yuv[1],
            V = yuv[2]
        };
    }

    public static HSL ParseHSL(string s, IFormatProvider? provider)
    {
        double[] hsl = ParseFunctionForm<double>(s, "hsl", provider);
        return new HSL
        {
            H = hsl[0],
            S = hsl[1],
            L = hsl[2]
        };
    }

    public static CMYK ParseCMYK(string s, IFormatProvider? provider)
    {
        double[] cmyk = ParseFunctionForm<double>(s, "cmyk", provider, 4);
        return new CMYK
        {
            C = cmyk[0],
            M = cmyk[1],
            Y = cmyk[2],
            K = cmyk[3]
        };
    }

    public static CieXYZ ParseCieXYZ(string s, IFormatProvider? provider)
    {
        double[] xyz = ParseFunctionForm<double>(s, "xyz", provider);
        return new CieXYZ
        {
            X = xyz[0],
            Y = xyz[1],
            Z = xyz[2]
        };
    }
}
