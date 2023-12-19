using CalculatorShell.Engine;
using CalculatorShell.Engine.Algortihms;

namespace Calculator.Tests.Engine.Algorithms;

internal class DoublesTests
{
    [TestCase(0.0, 0.0, AngleSystem.Deg)]
    [TestCase(90.0, 1.0, AngleSystem.Deg)]
    [TestCase(180.0, 0.0, AngleSystem.Deg)]
    [TestCase(270.0, -1.0, AngleSystem.Deg)]
    [TestCase(360.0, 0.0, AngleSystem.Deg)]
    [TestCase(0.0, 0.0, AngleSystem.Grad)]
    [TestCase(100.0, 1.0, AngleSystem.Grad)]
    [TestCase(200.0, 0.0, AngleSystem.Grad)]
    [TestCase(300.0, -1.0, AngleSystem.Grad)]
    [TestCase(400.0, 0.0, AngleSystem.Grad)]
    public void Test_SinCorrected(double input, double expected, AngleSystem angleSystem)
    {
        var result = Doubles.SinCorrected(input, angleSystem);
        Assert.That(result, Is.EqualTo(expected).Within(1E-16));
    }

    [TestCase(0.0, 1.0, AngleSystem.Deg)]
    [TestCase(90.0, 0.0, AngleSystem.Deg)]
    [TestCase(180.0, -1.0, AngleSystem.Deg)]
    [TestCase(270.0, 0.0, AngleSystem.Deg)]
    [TestCase(360.0, 1.0, AngleSystem.Deg)]
    [TestCase(0.0, 1.0, AngleSystem.Grad)]
    [TestCase(100.0, 0.0, AngleSystem.Grad)]
    [TestCase(200.0, -1.0, AngleSystem.Grad)]
    [TestCase(300.0, 0.0, AngleSystem.Grad)]
    [TestCase(400.0, 1.0, AngleSystem.Grad)]
    public void Test_CosCorrected(double input, double expected, AngleSystem angleSystem)
    {
        var result = Doubles.CosCorrected(input, angleSystem);
        Assert.That(result, Is.EqualTo(expected).Within(1E-16));
    }

    [TestCase(0.0, 0, AngleSystem.Deg)]
    [TestCase(45.0, 1, AngleSystem.Deg)]
    [TestCase(90.0, double.PositiveInfinity, AngleSystem.Deg)]
    [TestCase(135, -1, AngleSystem.Deg)]
    [TestCase(180.0, 0, AngleSystem.Deg)]
    [TestCase(225.0, 1.0, AngleSystem.Deg)]
    [TestCase(270.0, double.PositiveInfinity, AngleSystem.Deg)]
    [TestCase(315.0, -1, AngleSystem.Deg)]
    [TestCase(360.0, 0, AngleSystem.Deg)]
    public void Test_TanCorrected(double input, double expected, AngleSystem angleSystem)
    {
        var result = Doubles.TanCorrected(input, angleSystem);
        Assert.That(result, Is.EqualTo(expected).Within(1E-16));
    }
}
