namespace CalculatorShell.Engine.Colors;

public static class Extensions
{
    private static int RoundToInt(double v)
        => (int)Math.Round(v, 2);

    public static string ToHex(this RGB rGB)
    {
        byte[] data =
        [
            (byte)rGB.R,
            (byte)rGB.G,
            (byte)rGB.B,
        ];
        return $"#{Convert.ToHexString(data)}";
    }

    public static RGB ToRgb(this HSL hSL)
    {
        if (hSL.S == 0)
        {
            return new RGB
            {
                R = 255,
                G = 255,
                B = 255
            };
        }
        else
        {
            double q = (hSL.L < 0.5) ? (hSL.L * (1.0 + hSL.S)) : (hSL.L + hSL.S - (hSL.L * hSL.S));
            double p = (2.0 * hSL.L) - q;

            double Hk = hSL.H / 360.0;
            double[] T =
            [
                Hk + (1.0 / 3.0),    // Tr
                Hk,              // Tb
                Hk - (1.0 / 3.0),    // Tg
            ];
            for (int i = 0; i < 3; i++)
            {
                if (T[i] < 0) T[i] += 1.0;
                if (T[i] > 1) T[i] -= 1.0;

                if ((T[i] * 6) < 1)
                {
                    T[i] = p + ((q - p) * 6.0 * T[i]);
                }
                else if ((T[i] * 2.0) < 1) //(1.0/6.0)<=T[i] && T[i]<0.5
                {
                    T[i] = q;
                }
                else if ((T[i] * 3.0) < 2) // 0.5<=T[i] && T[i]<(2.0/3.0)
                {
                    T[i] = p + (q - p) * ((2.0 / 3.0) - T[i]) * 6.0;
                }
                else
                {
                    T[i] = p;
                }
            }

            return new RGB
            {
                R = RoundToInt(T[0] * 255.0),
                G = RoundToInt(T[1] * 255.0),
                B = RoundToInt(T[2] * 255.0),
            };
        }
    }

    public static HSL ToHsl(this RGB rGB)
    {
        double h = 0, s = 0, l = 0;

        // normalizes red-green-blue values
        double nRed = rGB.R / 255.0;
        double nGreen = rGB.G / 255.0;
        double nBlue = rGB.B / 255.0;

        double max = Math.Max(nRed, Math.Max(nGreen, nBlue));
        double min = Math.Min(nRed, Math.Min(nGreen, nBlue));

        // hue
        if (max == min)
        {
            h = 0; // undefined
        }
        else if (max == nRed && nGreen >= nBlue)
        {
            h = 60.0 * (nGreen - nBlue) / (max - min);
        }
        else if (max == nRed && nGreen < nBlue)
        {
            h = 60.0 * (nGreen - nBlue) / (max - min) + 360.0;
        }
        else if (max == nGreen)
        {
            h = 60.0 * (nBlue - nRed) / (max - min) + 120.0;
        }
        else if (max == nBlue)
        {
            h = 60.0 * (nRed - nGreen) / (max - min) + 240.0;
        }

        // luminance
        l = (max + min) / 2.0;

        // saturation
        if (l == 0 || max == min)
        {
            s = 0;
        }
        else if (0 < l && l <= 0.5)
        {
            s = (max - min) / (max + min);
        }
        else if (l > 0.5)
        {
            s = (max - min) / (2 - (max + min)); //(max-min > 0)?
        }

        return new HSL
        {
            H = Math.Round(h, 2),
            S = Math.Round(s, 2),
            L = Math.Round(l, 2),
        };
    }

    public static CMYK ToCmyk(this RGB rGB)
    {
        double c = (255 - rGB.R) / 255.0;
        double m = (255 - rGB.G) / 255.0;
        double y = (255 - rGB.B) / 255.0;

        double min = Math.Min(c, Math.Min(m, y));
        if (min == 1.0)
        {
            return new CMYK
            {
                C = 0,
                M = 0,
                Y = 0,
                K = 1,
            };
        }

        return new CMYK
        {
            C = (c - min) / (1 - min),
            M = (m - min) / (1 - min),
            Y = (y - min) / (1 - min),
            K = min
        };
    }

    public static RGB ToRgb(this CMYK cMYK)
    {
        return new RGB
        {
            R = Convert.ToInt32((1.0 - cMYK.C) * (1.0 - cMYK.K) * 255.0),
            G = Convert.ToInt32((1.0 - cMYK.M) * (1.0 - cMYK.K) * 255.0),
            B = Convert.ToInt32((1.0 - cMYK.Y) * (1.0 - cMYK.K) * 255.0),
        };
    }

    public static YUV ToYuv(this RGB rGB)
    {

        // normalizes red/green/blue values
        double nRed = (double)rGB.R / 255.0;
        double nGreen = (double)rGB.G / 255.0;
        double nBlue = (double)rGB.B / 255.0;

        return new YUV
        {
            Y = 0.299 * nRed + 0.587 * nGreen + 0.114 * nBlue,
            U = -0.1471376975169300226 * nRed - 0.2888623024830699774 * nGreen + 0.436 * nBlue,
            V = 0.615 * nRed - 0.5149857346647646220 * nGreen - 0.1000142653352353780 * nBlue,
        };
    }

    public static RGB ToRgb(this YUV yUV)
    {
        return new RGB
        {
            R = Convert.ToInt32((yUV.Y + 1.139837398373983740 * yUV.V) * 255),
            G = Convert.ToInt32((yUV.Y - 0.3946517043589703515 * yUV.U - 0.5805986066674976801 * yUV.V) * 255),
            B = Convert.ToInt32((yUV.Y + 2.032110091743119266 * yUV.U) * 255)
        };
    }

    public static CieXYZ ToXyz(this RGB rGB)
    {
        // normalize red, green, blue values
        double rLinear = rGB.R / 255.0;
        double gLinear = rGB.G / 255.0;
        double bLinear = rGB.B / 255.0;

        // convert to a sRGB form
        double r = (rLinear > 0.04045) ? Math.Pow((rLinear + 0.055) / (1 + 0.055), 2.2) : (rLinear / 12.92);
        double g = (gLinear > 0.04045) ? Math.Pow((gLinear + 0.055) / (1 + 0.055), 2.2) : (gLinear / 12.92);
        double b = (bLinear > 0.04045) ? Math.Pow((bLinear + 0.055) / (1 + 0.055), 2.2) : (bLinear / 12.92);

        return new CieXYZ
        {
            X = (r * 0.4124 + g * 0.3576 + b * 0.1805),
            Y = (r * 0.2126 + g * 0.7152 + b * 0.0722),
            Z = (r * 0.0193 + g * 0.1192 + b * 0.9505)
        };
    }

    public static RGB ToRgb(this CieXYZ cieXYZ)
    {
        double[] cLinear =
        [
            cieXYZ.X * 3.2410 - cieXYZ.Y * 1.5374 - cieXYZ.Z * 0.4986, // red
            -cieXYZ.X * 0.9692 + cieXYZ.Y * 1.8760 - cieXYZ.Z * 0.0416, // green
            cieXYZ.X * 0.0556 - cieXYZ.Y * 0.2040 + cieXYZ.Z * 1.0570, // blue
        ];
        for (int i = 0; i < 3; i++)
        {
            cLinear[i] = (cLinear[i] <= 0.0031308) ? 12.92 * cLinear[i] : (1 + 0.055) * Math.Pow(cLinear[i], (1.0 / 2.4)) - 0.055;
        }

        return new RGB
        {
            R = RoundToInt(cLinear[0] * 255.0),
            G = RoundToInt(cLinear[1] * 255.0),
            B = RoundToInt(cLinear[2] * 255.0),
        };
    }
}
