//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Numerics;

namespace CalculatorShell.Engine.MathComponents;

public static class EquationSolver
{
    private const double Delta = 0.00000000000001;

    public static EquationSolution FindRoots(double x4, double x3, double x2, double x1, double x0)
    {
        EquationSolution roots = new();
        if (x4 != 0.0)
        {
            roots.AddNormalizedRange(Quartic(x3 / x4, x2 / x4, x1 / x4, x0 / x4));
        }
        else if (x3 != 0.0)
        {
            roots.AddNormalizedRange(Cubic(x2 / x3, x1 / x3, x0 / x3));
        }
        else if (x2 != 0.0)
        {
            roots.AddNormalizedRange(Quadratic(x1 / x2, x0 / x2));
        }
        else if (x1 != 0.0)
        {
            roots.AddNormalizedRange(Linear(x0 / x1));
        }
        else
        {
            roots.Clear();
        }

        return roots;
    }

    private static IEnumerable<Complex> Linear(double a)
    {
        yield return -a;
    }

    private static IEnumerable<Complex> Quadratic(double b, double c)
    {
        Complex cplx = Complex.Sqrt(b * b - 4.0 * c);
        yield return (cplx - b) / 2.0;
        yield return (b + cplx) / -2.0;
    }

    private static IEnumerable<Complex> Cubic(double a, double b, double c)
    {
        const double SQRT2_DIV_3 = 0.86602540378443864676;

        double d = c;
        c = b;
        b = a;
        a = 1.0;

        double delta_0 = b * b - 3.0 * a * c;
        double delta_1 = 2.0 * b * b * b - 9.0 * a * b * c + 27.0 * a * a * d;

        if (Math.Abs(delta_0) <= Delta && Math.Abs(delta_1) <= Delta)
        {
            Complex root = new(-b / (3.0 * a), 0.0);
            yield return root;
            yield break;
        }

        Complex extra0 = Complex.Pow(delta_1 * delta_1 - 4.0 * delta_0 * delta_0 * delta_0, 1.0 / 2.0);

        Complex C = Complex.Pow((delta_1 + extra0) / 2.0, 1.0 / 3.0);
        if (Complex.Abs(C) <= Delta) C = Complex.Pow((delta_1 - extra0) / 2.0, 1.0 / 3.0); // delta_0 == 0

        Complex xi = new(-0.5, SQRT2_DIV_3);

        yield return x_k(0);
        yield return x_k(1);
        yield return x_k(2);

        Complex x_k(int k)
        {
            Complex xiRaisedToK = xi;
            for (int i = 0; i < k; i++) xiRaisedToK *= xi;
            return -1.0 / (3.0 * a) * (b + xiRaisedToK * C + delta_0 / (xiRaisedToK * C));
        }
    }

    private static IEnumerable<Complex> Quartic(double a, double b, double c, double d)
    {
        double e = d;
        d = c;
        c = b;
        b = a;
        a = 1.0;

        Complex root1 = new();
        Complex root2 = new();
        Complex root3 = new();
        Complex root4 = new();

        if (b != 0.0 || d != 0.0)
        {
            if ((c == b * b / 4.0 + 2.0 * Math.Sqrt(e) && d == b * Math.Sqrt(e)) || (c == b * b / 4.0 - 2.0 * Math.Sqrt(e) && d == -b * Math.Sqrt(e)))
            {
                double aa = b / 2.0;
                double kk = d / (2 * aa);
                if (aa * aa - 4 * kk < 0)
                {
                    double rad = Math.Sqrt(4.0 * kk - aa * aa) / 2.0;
                    double cad = -aa / 2.0;

                    yield return new(cad, rad);
                    yield return new(cad, -rad);
                    yield break;
                }
                else if (aa * aa - 4.0 * kk >= 0.0)
                {
                    double rad = Math.Sqrt(aa * aa - 4.0 * kk) / 2.0;
                    double cad = -aa / 2.0;

                    yield return new(cad - rad, 0.0);
                    yield return new(cad + rad, 0.0);
                    yield break;
                }
            }
            else
            {
                double y = Rescubic(b, c, d, e);
                double A = b * b / 4.0 - c + 2.0 * y;
                double r = (b * y - d) / (-2.0 * A);

                double qc1a;
                double qc1b;
                double qc2a;
                double qc2b;
                if (A > 0.0)
                {
                    qc1a = b / 2.0 - Math.Sqrt(A);
                    qc1b = y + Math.Sqrt(A) * r;
                    qc2a = b / 2.0 + Math.Sqrt(A);
                    qc2b = y - Math.Sqrt(A) * r;
                }
                else
                {
                    qc1a = b / 2.0;
                    qc1b = y + Math.Sqrt(y * y - e);
                    qc2a = b / 2.0;
                    qc2b = y - Math.Sqrt(y * y - e);
                }

                double jim = -qc1a / 2.0;
                double bob = qc1a * qc1a - 4.0 * qc1b;
                if (bob < 0.0)
                {
                    double rad = Math.Sqrt(4.0 * qc1b - qc1a * qc1a) / 2.0;
                    root1 = new(jim, rad);
                    root2 = new(jim, -rad);
                }
                if (bob >= 0)
                {
                    double rad = jim + Math.Sqrt(bob) / 2.0;
                    double rod = jim - Math.Sqrt(bob) / 2.0;
                    root1 = new(rad, 0.0);
                    root2 = new(rod, 0.0);
                }

                double jen = -0.5 * qc2a;
                double bab = qc2a * qc2a - 4.0 * qc2b;
                if (bab < 0.0)
                {
                    double royd = 0.5 * Math.Sqrt(4.0 * qc2b - qc2a * qc2a);
                    root3 = new(jen, royd);
                    root4 = new(jen, -royd);
                }
                if (bab >= 0)
                {
                    double royd = jen + 0.5 * Math.Sqrt(bab);
                    double rood = jen - 0.5 * Math.Sqrt(bab);
                    root3 = new(royd, 0.0);
                    root4 = new(rood, 0.0);
                }
            }

        }
        else
        {
            if (c * c - 4.0 * e >= 0.0)
            {
                if (-c + Math.Sqrt(c * c - 4.0 * e) >= 0.0)
                {
                    root1 = new(Math.Sqrt(-c / 2 + 0.5 * Math.Sqrt(c * c - 4 * e)), 0.0);
                    root2 = new(-Math.Sqrt(-c / 2 + 0.5 * Math.Sqrt(c * c - 4 * e)), 0.0);
                }
                else
                {
                    root1 = new(0.0, Math.Sqrt(c / 2 - 0.5 * Math.Sqrt(c * c - 4 * e)));
                    root2 = new(0.0, -Math.Sqrt(c / 2 - 0.5 * Math.Sqrt(c * c - 4 * e)));
                }

                if (-c - Math.Sqrt(c * c - 4.0 * e) >= 0.0)
                {
                    root3 = new(Math.Sqrt(-c / 2 - 0.5 * Math.Sqrt(c * c - 4 * e)), 0.0);
                    root4 = new(-Math.Sqrt(-c / 2 - 0.5 * Math.Sqrt(c * c - 4 * e)), 0.0);
                }
                else
                {
                    root3 = new(0.0, Math.Sqrt(c / 2 + 0.5 * Math.Sqrt(c * c - 4 * e)));
                    root4 = new(0.0, -Math.Sqrt(c / 2 + 0.5 * Math.Sqrt(c * c - 4 * e)));
                }
            }

            if (c * c - 4.0 * e < 0.0)
            {
                double Az = -c / 2.0;
                double Bz = 0.5 * Math.Sqrt(4.0 * e - c * c);
                double y0 = Math.Sqrt(0.5 * (Math.Sqrt(Az * Az + Bz * Bz) - Az));

                root1 = new(0.5 * Bz / y0, y0);
                root2 = new(0.5 * Bz / y0, -y0);
                root3 = new(-0.5 * Bz / y0, y0);
                root4 = new(-0.5 * Bz / y0, -y0);
            }
        }

        yield return root1;
        yield return root2;
        yield return root3;
        yield return root4;

        static double Rescubic(double r, double s, double t, double u)
        {
            double xx = 0.0;

            double bb = -s / 2.0;
            double cc = r * t / 4.0 - u;
            double dd = s * u / 2.0 - t * t / 8.0 - r * r * u / 8.0;

            double disc = (18.0 * bb * cc * dd) - (4.0 * bb * bb * bb * dd) + (bb * bb * cc * cc) - (4.0 * cc * cc * cc) - (27.0 * dd * dd);
            double pp = cc - (bb * bb / 3.0);
            double qq = ((2.0 / 27.0) * (bb * bb * bb)) - (bb * cc / 3.0) + dd;
            double ff = (27.0 / 2.0) * qq;

            if (disc > 0.0)
            {
                double x1 = -bb / 3.0 + 2.0 * Math.Sqrt(-pp / 3.0) * Math.Cos((1.0 / 3.0) * Math.Acos((1.5 * qq / pp) * Math.Sqrt(-3.0 / pp)));
                double x2 = -bb / 3.0 + 2.0 * Math.Sqrt(-pp / 3.0) * Math.Cos((1.0 / 3.0) * Math.Acos((1.5 * qq / pp) * Math.Sqrt(-3.0 / pp)) + (2.0 / 3.0) * Math.PI);
                double x3 = -bb / 3.0 + 2.0 * Math.Sqrt(-pp / 3.0) * Math.Cos((1.0 / 3.0) * Math.Acos((1.5 * qq / pp) * Math.Sqrt(-3.0 / pp)) - (2.0 / 3.0) * Math.PI);
                double g1 = Math.Max(x1, Math.Max(x2, x3));
                double g3 = Math.Min(x1, Math.Min(x2, x3));
                double g2 = x1 + x2 + x3 - g1 - g3;
                if (r * r / 4.0 - s + 2.0 * g1 > 0.0)
                {
                    xx = g1;
                }
                else
                {
                    if (r * r / 4 - s + 2 * g2 > 0) xx = g2;
                    else xx = g3;
                }
            }

            if (disc == 0)
            {
                double x1 = -bb / 3.0 - (2.0 / 3.0) * Math.Cbrt(ff);
                double x2 = -bb / 3.0 + (1.0 / 3.0) * Math.Cbrt(ff);
                double g1 = Math.Max(x1, x2);
                double g2 = Math.Min(x1, x2);
                if (r * r / 4.0 - s + 2.0 * g1 > 0.0) xx = g1;
                else xx = g2;
            }

            if (disc < 0)
            {
                double g1 = -bb / 3.0 - (1.0 / 3.0) * Math.Cbrt(ff + 0.5 * Math.Sqrt(-27.0 * disc)) - (1.0 / 3.0) * Math.Cbrt(ff - 0.5 * Math.Sqrt(-27.0 * disc));
                xx = g1;
            }

            return xx;
        }
    }
}
