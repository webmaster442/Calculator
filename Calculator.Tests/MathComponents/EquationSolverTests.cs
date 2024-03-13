﻿using System.Numerics;

using CalculatorShell.Engine.MathComponents;

using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Calculator.Tests.MathComponents;

[TestFixture]
internal class EquationSolverTests
{
    public static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            //x4 x3 x2 x1 x0 
            yield return new TestCaseData(0, 0, 0, 0, 0, new Complex[] { } );
            yield return new TestCaseData(0, 0, 0, 1, 1, new Complex[] { new Complex(-1, 0) });
            yield return new TestCaseData(0, 0, 0, 1, -1, new Complex[] { new Complex(1, 0) });

            yield return new TestCaseData(0, 0, 1, 0, 0, new Complex[] { new Complex(0, 0) });
            yield return new TestCaseData(0, 0, 1, 4, 4, new Complex[] { new Complex(-2, 0) });
            yield return new TestCaseData(0, 0, 1, 2, -3, new Complex[] { new Complex(-3, 0), new Complex(1, 0) });

            yield return new TestCaseData(0, 1, 0, 0, 0, new Complex[] { new Complex(0, 0) });
            yield return new TestCaseData(0, 1, -9, 27, -27, new Complex[] { new Complex(3, 0) });
            yield return new TestCaseData(0, 1, -4, 5, -2, new Complex[] { new Complex(1, 0), new Complex(2, 0) });
            yield return new TestCaseData(0, 1, -6, 11, -6, new Complex[] { new Complex(1, 0), new Complex(2, 0), new Complex(3, 0) });

            yield return new TestCaseData(1, 2, - 41, -42, 360, new Complex[] { new Complex(-6, 0), new Complex(-4, 0), new Complex(3, 0), new Complex(5, 0) });
            yield return new TestCaseData(1, 0, -5, 0, 4, new Complex[] { new Complex(-2, 0), new Complex(-1, 0), new Complex(1, 0), new Complex(2, 0) });
            yield return new TestCaseData(1, 0, -1, 0, 0, new Complex[] { new Complex(-1, 0), new Complex(0, 0), new Complex(1, 0) });
            yield return new TestCaseData(1, 0, -2, 0, 1, new Complex[] { new Complex(-1, 0), new Complex(1, 0) });
            yield return new TestCaseData(1, -1, 0, 0, 0, new Complex[] { new Complex(0, 0), new Complex(1, 0) });
            yield return new TestCaseData(1, 0, 0, 0, 0, new Complex[] { new Complex(0, 0) });
            yield return new TestCaseData(1, -4, 6, -4, 1, new Complex[] { new Complex(1, 0) });

            yield return new TestCaseData(1, 0, 0, 0, -1, new Complex[] { new Complex(-1, 0), new Complex(1, 0), new Complex(0, -1), new Complex(0, 1) });
            yield return new TestCaseData(1, 0, 0, 0, -1, new Complex[] { new Complex(-1, 0), new Complex(1, 0), new Complex(0, -1), new Complex(0, 1) });
            yield return new TestCaseData(1, 0, 2, 0, 1, new Complex[] { new Complex(0, -1), new Complex(0, 1) });

            yield return new TestCaseData(1, 0, 5, 0, 4, new Complex[] { new Complex(0.0, -2.0), new Complex(0.0, -1.0), new Complex(0.0, 1.0), new Complex(0.0, 2.0) });
        }
    }

    [TestCaseSource(nameof(TestCases))]
    public void TestFindRoots(double x4, double x3, double x2, double x1, double x0, IEnumerable<Complex> expected)
    {
        var results = EquationSolver.FindRoots(x4, x3, x2, x1, x0);
        Assert.That(results, Is.EquivalentTo(expected));
    }
}