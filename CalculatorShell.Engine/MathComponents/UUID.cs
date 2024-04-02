using System.Text;

namespace CalculatorShell.Engine.MathComponents;

public sealed class UUID
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
}
