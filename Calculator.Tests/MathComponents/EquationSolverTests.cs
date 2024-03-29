//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Numerics;

using CalculatorShell.Engine.MathComponents;

namespace Calculator.Tests.MathComponents;

[TestFixture]
public class EquationSolverTests
{
    public static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            //x4 x3 x2 x1 x0 
            yield return new TestCaseData(0, 0, 0, 0, 0, new Complex[] { });
            yield return new TestCaseData(0, 0, 0, 1, 1, new Complex[] { new(-1, 0) });
            yield return new TestCaseData(0, 0, 0, 1, -1, new Complex[] { new(1, 0) });

            yield return new TestCaseData(0, 0, 1, 0, 0, new Complex[] { new(0, 0) });
            yield return new TestCaseData(0, 0, 1, 4, 4, new Complex[] { new(-2, 0) });
            yield return new TestCaseData(0, 0, 1, 2, -3, new Complex[] { new(-3, 0), new(1, 0) });

            yield return new TestCaseData(0, 1, 0, 0, 0, new Complex[] { new(0, 0) });
            yield return new TestCaseData(0, 1, -9, 27, -27, new Complex[] { new(3, 0) });
            yield return new TestCaseData(0, 1, -4, 5, -2, new Complex[] { new(1, 0), new(2, 0) });
            yield return new TestCaseData(0, 1, -6, 11, -6, new Complex[] { new(1, 0), new(2, 0), new(3, 0) });

            yield return new TestCaseData(1, 2, -41, -42, 360, new Complex[] { new(-6, 0), new(-4, 0), new(3, 0), new(5, 0) });
            yield return new TestCaseData(1, 0, -5, 0, 4, new Complex[] { new(-2, 0), new(-1, 0), new(1, 0), new(2, 0) });
            yield return new TestCaseData(1, 0, -1, 0, 0, new Complex[] { new(-1, 0), new(0, 0), new(1, 0) });
            yield return new TestCaseData(1, 0, -2, 0, 1, new Complex[] { new(-1, 0), new(1, 0) });
            yield return new TestCaseData(1, -1, 0, 0, 0, new Complex[] { new(0, 0), new(1, 0) });
            yield return new TestCaseData(1, 0, 0, 0, 0, new Complex[] { new(0, 0) });
            yield return new TestCaseData(1, -4, 6, -4, 1, new Complex[] { new(1, 0) });

            yield return new TestCaseData(1, 0, 0, 0, -1, new Complex[] { new(-1, 0), new(1, 0), new(0, -1), new(0, 1) });
            yield return new TestCaseData(1, 0, 0, 0, -1, new Complex[] { new(-1, 0), new(1, 0), new(0, -1), new(0, 1) });
            yield return new TestCaseData(1, 0, 2, 0, 1, new Complex[] { new(0, -1), new(0, 1) });

            yield return new TestCaseData(1, 0, 5, 0, 4, new Complex[] { new(0.0, -2.0), new(0.0, -1.0), new(0.0, 1.0), new(0.0, 2.0) });
        }
    }

    [TestCaseSource(nameof(TestCases))]
    public void TestFindRoots(double x4, double x3, double x2, double x1, double x0, IEnumerable<Complex> expected)
    {
        var results = EquationSolver.FindRoots(x4, x3, x2, x1, x0);
        Assert.That(results, Is.EquivalentTo(expected));
    }
}
