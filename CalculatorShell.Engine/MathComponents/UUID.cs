using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CalculatorShell.Engine.MathComponents;

public sealed partial class UUID : ICalculatorFormattable, IParsable<UUID>
{

    internal byte[] Bytes { get; }

    public bool IsLittleEndian { get; }

    internal UUID(byte[] data, bool isLittleEndian)
    {
        if (data.Length != 16)
            throw new ArgumentException("UUID must be 16 bytes long");
        Bytes = data;
        IsLittleEndian = isLittleEndian;
    }

    public UUID EndianSwap()
    {
        var data = new byte[16];
        for (int i = 0; i < 16; i += 4)
        {
            data[i] = Bytes[i + 3];
            data[i + 1] = Bytes[i + 2];
            data[i + 2] = Bytes[i + 1];
            data[i + 3] = Bytes[i];
        }
        return new UUID(data, !IsLittleEndian);
    }

    public override string ToString()
    {
        var sb = new StringBuilder(36);
        for (int i = 0; i < 16; i++)
        {
            if (i == 4 || i == 6 || i == 8 || i == 10)
            {
                sb.Append('-');
            }
            sb.Append(Bytes[i].ToString("X2"));
        }
        return sb.ToString();
    }

    public string ToString(CultureInfo culture, bool thousandsGrouping)
    {
        return
            $"""
            UUID: {ToString()}
            Base64: {Convert.ToBase64String(Bytes)}
            """;
    }



    public static UUID Parse(string s, IFormatProvider? provider)
    {
        if (UUIDMatch().IsMatch(s))
        {
            string hex = s.Replace("-", "");
            return new UUID(Convert.FromHexString(hex), true);
        }
        throw new FormatException("Invalid UUID format");
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out UUID result)
    {
        try
        {
            result = Parse(s, provider);
            return true;
        }
        catch (FormatException)
        {
            result = null;
            return false;
        }
    }

    [GeneratedRegex(" ^[0-9a-fA-F]{8}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{12}$")]
    private static partial Regex UUIDMatch();
}
