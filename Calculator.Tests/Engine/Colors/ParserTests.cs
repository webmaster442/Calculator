using System.Globalization;

using CalculatorShell.Engine.Colors;

namespace Calculator.Tests.Engine.Colors;

[TestFixture]
public class ParserTests
{
    [TestCase("#000000", 0, 0, 0)]
    [TestCase("#ff0000", 255, 0, 0)]
    [TestCase("#00ff00", 0, 255, 0)]
    [TestCase("#0000ff", 0, 0, 255)]
    [TestCase("rgb(11, 255, 33)", 11, 255, 33)]
    [TestCase("rGb(11,255,33)", 11, 255, 33)]
    public void ParseRgb_Works(string s, int r, int g, int b)
    {
        RGB expected = new()
        {
            R = r,
            G = g,
            B = b
        };

        RGB parsed = RGB.Parse(s, CultureInfo.InvariantCulture);

        Assert.That(parsed, Is.EqualTo(expected));
    }

    [TestCase("yuv(0.5, 0.1, 0.3)", 0.5, 0.1, 0.3)]
    public void ParseYUV_Works(string s, double y, double u, double v)
    {
        YUV expected = new()
        {
            Y = y,
            U = u,
            V = v
        };

        YUV parsed = YUV.Parse(s, CultureInfo.InvariantCulture);

        Assert.That(parsed, Is.EqualTo(expected));
    }

    [TestCase("hsl(125.6, 0.1, 0.3)", 125.6, 0.1, 0.3)]
    public void ParseHSL_Works(string str, double h, double s, double l)
    {
        HSL expected = new()
        {
            H = h,
            S = s,
            L = l,
        };

        HSL parsed = HSL.Parse(str, CultureInfo.InvariantCulture);

        Assert.That(parsed, Is.EqualTo(expected));
    }

    [TestCase("cmyk(0.5, 0.1, 0.3, 0.8)", 0.5, 0.1, 0.3, 0.8)]
    public void ParseCMYK_Works(string s, double c, double m, double y, double k)
    {
        CMYK expected = new()
        {
            C = c,
            M = m,
            Y = y,
            K = k
        };

        CMYK parsed = CMYK.Parse(s, CultureInfo.InvariantCulture);

        Assert.That(parsed, Is.EqualTo(expected));
    }

    [TestCase("xyz(0.5, 0.1, 0.3)", 0.5, 0.1, 0.3)]
    public void ParseXYZ_Works(string s, double x, double y, double z)
    {
        CieXYZ expected = new()
        {
            X = x,
            Y = y,
            Z = z
        };

        CieXYZ parsed = CieXYZ.Parse(s, CultureInfo.InvariantCulture);

        Assert.That(parsed, Is.EqualTo(expected));
    }
}
