//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

namespace CalculatorShell.Engine.MathComponents;

public sealed class NumberSystemConverter
{
    private readonly char[] _tokens;

    public NumberSystemConverter()
    {
        _tokens = "0123456789ABCDEFGHIJKLMNOPQRSTUVXYZ".ToCharArray();
    }

    public int MaxSystem => _tokens.Length;

    public string Convert(string input, int sourceSystem, int targetSystem)
    {
        if (string.IsNullOrEmpty(input))
            throw new EngineException($"{nameof(input)} must not be an empty or null string");

        if (sourceSystem < 2 || sourceSystem > _tokens.Length)
            throw new EngineException($"{nameof(sourceSystem)} must be at least 2 and maximum {MaxSystem}");

        if (targetSystem < 2 || targetSystem > _tokens.Length)
            throw new EngineException($"{nameof(targetSystem)} must be at least 2 and maximum {MaxSystem}");

        Int128 baseTen = ConvertToBaseTen(input.ToUpper(), sourceSystem);
        return ConvertToTargetSystem(baseTen, targetSystem);
    }

    private string ConvertToTargetSystem(Int128 baseTen, int targetSystem)
    {
        Stack<char> symbols = new Stack<char>();
        while (baseTen > Int128.Zero)
        {
            Int128 digit = baseTen % targetSystem;
            symbols.Push(_tokens[(int)digit]);

            baseTen /= targetSystem;
        }
        return ToString(symbols);
    }

    private static string ToString(Stack<char> symbols)
    {
        StringBuilder sb = new();
        while (symbols.Count > 0)
        {
            sb.Append(symbols.Pop());
        }
        return sb.ToString();
    }

    private Int128 ConvertToBaseTen(string input, int sourceSystem)
    {
        Int128 result = Int128.Zero;
        long factor = 1L;
        for (int i = input.Length - 1; i >= 0; i--)
        {
            char token = input[i];
            int tokenIndex = Array.IndexOf(_tokens, token);

            if (tokenIndex >= sourceSystem)
                throw new EngineException($"Invalid symbol: {token} for number system: {sourceSystem}");


            long value = tokenIndex * factor;
            result += value;
            factor *= sourceSystem;
        }

        return result;
    }
}
