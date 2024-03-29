//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

using CalculatorShell.Engine;

namespace Calculator.Tests.Engine;

[TestFixture]
public class LogicEngineTests
{
    private ILogicEngine _engine;

    [SetUp]
    public void Setup()
    {
        _engine = new LogicEngine();
    }

    [TestCase("!(!a)", "a")]
    [TestCase("true", "True")]
    [TestCase("false", "False")]
    [TestCase("a | a", "a")]
    [TestCase("a | b", "(a) | (b)")]
    [TestCase("a & b", "(a) & (b)")]
    [TestCase("a & a", "a")]
    [TestCase("a & true", "a")]
    [TestCase("a & false", "False")]
    [TestCase("a | true", "True")]
    [TestCase("a | false", "a")]
    [TestCase("(!a & b) | (a & b)", "b")]
    [TestCase("(a & !b) | (!a & !b)", "!(b)")]
    [TestCase("(a & !b) | (a & b)", "a")]
    [TestCase("(!a & !b) | (!a & b)", "!(a)")]
    public void Parse_Simplifies_WhenOk(string input, string expected)
    {
        var expression = _engine.Parse(input).Simplify();
        Assert.That(expression.ToString(CultureInfo.InvariantCulture), Is.EqualTo(expected));
    }

    [TestCase("a", 2, 3)]
    [TestCase("b", 1, 3)]
    [TestCase("(a) | (b)", 1, 2, 3)]
    [TestCase("(a) & (b)", 3)]
    public void Parse_Simplifies_Minterms_WhenOk(string expected, params int[] minterms)
    {
        var expression = _engine.Parse(2, minterms).Simplify();
        Assert.That(expression.ToString(CultureInfo.InvariantCulture), Is.EqualTo(expected));
    }

    [TestCase("0", false)]
    [TestCase("no", false)]
    [TestCase("1", true)]
    [TestCase("yes", true)]
    [TestCase("!true", false)]
    [TestCase("true & true", true)]
    [TestCase("true & false", false)]
    [TestCase("false & true", false)]
    [TestCase("false & false", false)]
    [TestCase("true | true", true)]
    [TestCase("true | false", true)]
    [TestCase("false | true", true)]
    [TestCase("false | false", false)]
    [TestCase("a & d", false)]
    [TestCase("(true | false) & true", true)]
    [TestCase("(false & false) | true", true)]
    public void Evaluate_RetursCorrectValue(string input, bool expected)
    {
        var expression = _engine.Parse(input);
        bool result = expression.Evaluate(new Dictionary<string, bool>
        {
            { "a", true },
            { "d", false },
        });
        Assert.That(result, Is.EqualTo(expected));
    }
}
