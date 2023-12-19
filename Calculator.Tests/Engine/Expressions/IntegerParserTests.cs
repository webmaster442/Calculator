using CalculatorShell.Engine.Expressions;

namespace Calculator.Tests.Engine.Expressions;

public class IntegerParserTests
{
    [TestCase("12345678", 12345678)]
    [TestCase("1234_5678", 12345678)]
    [TestCase("HxFF", 255)]
    [TestCase("HxF_F", 255)]
    [TestCase("Bx1010", 10)]
    [TestCase("Bx10_10", 10)]
    [TestCase("Ox123", 83)]
    [TestCase("Ox12_3", 83)]
    public void TryParse_AcceptedFormats_ResultsTrue(string input, long expected)
    {
        bool result = IntegerParser.TryParse(input, null, out Int128 parsed);

        long convered = (long)parsed;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(convered, Is.EqualTo(expected));
        });
    }
}