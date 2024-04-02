//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

namespace CalculatorShell.Engine.MathComponents;

public sealed class HashResult : IEquatable<HashResult?>, ICalculatorFormattable
{
    private readonly byte[] _hash;

    public HashResult(byte[] hash)
    {
        _hash = hash;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as HashResult);
    }

    public bool Equals(HashResult? other)
    {
        if (other is null)
            return false;

        if (_hash.Length != other._hash.Length)
            return false;

        for (int i = 0; i < _hash.Length; i++)
        {
            if (_hash[i] != other._hash[i])
                return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        HashCode h = new();
        foreach (var @byte in _hash)
        {
            h.Add(@byte);
        }
        return h.ToHashCode();
    }

    public string ToString(CultureInfo culture, bool thousandsGrouping)
    {
        return $"hex: {Convert.ToHexString(_hash)}\r\nbase64: {Convert.ToBase64String(_hash)}";
    }

    public static bool operator ==(HashResult? left, HashResult? right)
    {
        return EqualityComparer<HashResult>.Default.Equals(left, right);
    }

    public static bool operator !=(HashResult? left, HashResult? right)
    {
        return !(left == right);
    }
}
