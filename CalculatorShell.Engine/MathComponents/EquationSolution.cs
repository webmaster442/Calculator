//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace CalculatorShell.Engine.MathComponents;

public class EquationSolution : ICalculatorFormattable, IEnumerable<Complex>
{
    private static Complex Normalize(Complex input)
    {
        double newReal = Math.Round(input.Real, 14);
        double newImaginary = Math.Round(input.Imaginary, 14);
        return new Complex(newReal, newImaginary);
    }

    private readonly HashSet<Complex> _solutions;

    public EquationSolution()
    {
        _solutions = new();
    }

    internal void AddNormalizedRange(IEnumerable<Complex> items)
    {
        foreach (var item in items)
        {
            _ = _solutions.Add(Normalize(item));
        }
    }

    internal void Clear()
    {
        _solutions.Clear();
    }

    public string ToString(CultureInfo culture, bool thousandsGrouping)
    {
        StringBuilder sb = new();
        foreach (var item in _solutions)
        {
            _ = sb.AppendLine(Format(item, culture, thousandsGrouping));
        }
        return sb.ToString();
    }

    private string? Format(Complex item, CultureInfo culture, bool thousandsGrouping)
    {
        if (Math.Abs(item.Imaginary) < 1E-14)
            return NumberFomatter.ToString(item.Real, culture, thousandsGrouping);
        else
            return NumberFomatter.ToString(item, culture, thousandsGrouping);
    }

    public IEnumerator<Complex> GetEnumerator()
        => _solutions.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _solutions.GetEnumerator();
}
