using System.Diagnostics;
using System.Text;

namespace CalculatorShell.Engine.MathComponents;
public static class BcdConverter
{
    public static string BcdEncode(Int128 number)
    {
        StringBuilder result = new();
        foreach (var chr in number.ToString())
        {
            result.Append(Encode(chr));
            result.Append(' ');
        }
        return result.ToString().Trim();
    }

    public static string BcdDecode(string str)
    {
        StringBuilder result = new();
        foreach (var digit in str.Split(new char[] { ' ', '_', '\t', '-' }, StringSplitOptions.RemoveEmptyEntries))
        {
            result.Append(Decode(digit));
        }
        return result.ToString();
    }

    private static string Encode(char chr)
    {
        return chr switch
        {
            '0' => "0000",
            '1' => "0001",
            '2' => "0010",
            '3' => "0011",
            '4' => "0100",
            '5' => "0101",
            '6' => "0110",
            '7' => "0111",
            '8' => "1000",
            '9' => "1001",
            _ => throw new UnreachableException(),
        };
    }

    private static char Decode(string binary)
    {
        return binary switch
        {
            "0000" => '0',
            "0001" => '1',
            "0010" => '2',
            "0011" => '3',
            "0100" => '4',
            "0101" => '5',
            "0110" => '6',
            "0111" => '7',
            "1000" => '8',
            "1001" => '9',
            _ => throw new UnreachableException(),
        };
    }
}
