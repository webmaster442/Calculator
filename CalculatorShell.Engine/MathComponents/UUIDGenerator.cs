//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;

namespace CalculatorShell.Engine.MathComponents;

public static class UUIDGenerator
{
    public static UUID GenerateVersion3(UUID @namespace, string name)
    {
        if (@namespace.IsLittleEndian)
        {
            @namespace = @namespace.EndianSwap();
        }

        var data = @namespace.Bytes.Concat(Encoding.UTF8.GetBytes(name)).ToArray();
        var hashed = MD5.HashData(data);

        var result = new byte[16];

        Array.Copy(hashed, result, 16);

        result[6] = (byte)((result[6] & 0x0F) | 0x30);
        result[8] = (byte)((result[8] & 0x3F) | 0x80);

        return new UUID(result, BitConverter.IsLittleEndian);
    }

    public static UUID GenerateVersion5(UUID @namespace, string name)
    {
        if (@namespace.IsLittleEndian)
        {
            @namespace = @namespace.EndianSwap();
        }

        var data = @namespace.Bytes.Concat(Encoding.UTF8.GetBytes(name)).ToArray();
        var hashed = SHA1.HashData(data);

        var result = new byte[16];

        Array.Copy(hashed, result, 16);

        result[6] = (byte)((result[6] & 0x0F) | 0x50);
        result[8] = (byte)((result[8] & 0x3F) | 0x80);

        return new UUID(result, BitConverter.IsLittleEndian);
    }

    public static UUID GenerateVersion4()
    {
        var data = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(data);
        }

        data[6] = (byte)((data[6] & 0x0F) | 0x40);
        data[8] = (byte)((data[8] & 0x3F) | 0x80);

        return new UUID(data, BitConverter.IsLittleEndian);
    }

    private static readonly DateTime UuidTime = new(1582, 10, 15, 0, 0, 0, DateTimeKind.Utc);
    private static long lastTimestamp = 0;
    private static short clockSequence = (short)new Random().Next(0x10000);

    public static UUID GenerateVersion1()
    {
        byte[] uuidBytes = new byte[16];

        long timestamp = (DateTime.UtcNow - UuidTime).Ticks / 10L;
        if (timestamp <= lastTimestamp)
        {
            clockSequence++;
        }
        lastTimestamp = timestamp;

        // Time low
        Array.Copy(BitConverter.GetBytes((int)(timestamp & 0xFFFFFFFF)), 0, uuidBytes, 0, 4);

        // Time mid
        Array.Copy(BitConverter.GetBytes((short)((timestamp >> 32) & 0xFFFF)), 0, uuidBytes, 4, 2);

        // Time high and version
        Array.Copy(BitConverter.GetBytes((short)((timestamp >> 48) & 0x0FFF | 0x1000)), 0, uuidBytes, 6, 2);

        // Clock sequence
        Array.Copy(BitConverter.GetBytes(clockSequence), 0, uuidBytes, 8, 2);

        // Node (MAC address)
        byte[] node = new byte[6];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(node);
        }
        node.CopyTo(uuidBytes, 10);

        uuidBytes[8] = (byte)((uuidBytes[8] & 0x3F) | 0x80);

        return new UUID(uuidBytes, BitConverter.IsLittleEndian);
    }
}
