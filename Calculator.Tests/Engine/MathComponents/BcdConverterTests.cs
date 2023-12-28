using CalculatorShell.Engine.MathComponents;

namespace Calculator.Tests.Engine.MathComponents;

[TestFixture]
public class BcdConverterTests
{
    [TestCase(10, "0001 0000")]
    [TestCase(99, "1001 1001")]
    [TestCase(42, "0100 0010")]
    [TestCase(35, "0011 0101")]
    [TestCase(67, "0110 0111")]
    [TestCase(89, "1000 1001")]
    public void TestEncode(int input, string expected)
    {
        string result = BcdConverter.BcdEncode(input);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("10", "0001 0000")]
    [TestCase("99", "1001 1001")]
    [TestCase("42", "0100 0010")]
    [TestCase("35", "0011 0101")]
    [TestCase("67", "0110 0111")]
    [TestCase("89", "1000 1001")]
    public void TestDecode(string expected, string input)
    {
        string result = BcdConverter.BcdDecode(input);
        Assert.That(result, Is.EqualTo(expected));
    }
}
