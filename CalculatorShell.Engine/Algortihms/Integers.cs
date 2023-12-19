namespace CalculatorShell.Engine.Algortihms;

internal static class Integers
{
    public static Int128 Abs(Int128 value)
    {
        checked
        {
            if (value < Int128.Zero)
                return -1 * value;

            return value;
        }
    }

    public static Int128 Lcm(Int128 a, Int128 b)
    {
        checked
        {
            return (a * b) / Gcd(a, b);
        }
    }

    public static Int128 Gcd(Int128 a, Int128 b)
    {
        checked
        {
            if (a == 0 || b == 0)
            {
                return Abs(a) + Abs(b);
            }
            a = Abs(a);
            b = Abs(b);
            int shift = 0;
            while (((a | b) & 1) == 0)
            {
                a >>= 1;
                b >>= 1;
                shift++;
            }
            while ((a & 1) == 0)
            {
                a >>= 1;
            }
            do
            {
                while ((b & 1) == 0)
                {
                    b >>= 1;
                }
                if (a > b)
                {
                    (b, a) = (a, b);
                }
                b -= a;
            } while (b != 0);
            return a << shift;
        }
    }


    public static Int128 Factorial(Int128 number)
    {
        checked
        {
            if (number < 0)
                throw new ArgumentException("Must be positive", nameof(number));

            if (number == 0)
                return Int128.Zero;

            Int128 result = Int128.One;

            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}
