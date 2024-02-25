//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Engine;
using CalculatorShell.Engine.MathComponents;

namespace Calculator.Tests.Engine.MathComponents;

[TestFixture]
public class NumberSystemConverterTests
{
    private NumberSystemConverter _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new NumberSystemConverter();
    }

    [TestCase("ff", 16, 2, "11111111")]
    [TestCase("1010", 2, 16, "A")]
    [TestCase("1024", 10, 8, "2000")]
    public void Convert_ReturnsCorrectValue(string input, int source, int target, string expected)
    {
        string result = _sut.Convert(input, source, target);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("1", 1, 16)]
    [TestCase("1", 99, 16)]
    [TestCase("1", 2, 1)]
    [TestCase("1", 2, 99)]
    public void Convert_ThrowsArgumentOutOfRangeException_InvalidSystems(string input, int source, int target)
    {
        Assert.Throws<EngineException>(() =>
        {
            _sut.Convert(input, source, target);
        });
    }

    [Test]
    public void Convert_ThrowsArgumentException_EmptyInput()
    {
        Assert.Throws<EngineException>(() =>
        {
            _sut.Convert("", 2, 8);
        });
    }

    [Test]
    public void Convert_InvalidOperationException_InvalidSymbol_ForSystem()
    {
        Assert.Throws<EngineException>(() =>
        {
            _sut.Convert("01012", 2, 8);
        });
    }
}
