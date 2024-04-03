//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.IO.Hashing;

namespace CalculatorShell.Engine.MathComponents.Hashing;

public sealed class Fnv64 : NonCryptographicHashAlgorithm
{
    private const ulong OffsetBasis = 14695981039346656037;
    private const ulong Prime = 1099511628211;
    private ulong _state;

    public Fnv64() : base(sizeof(ulong))
    {
        _state = OffsetBasis;
    }

    public override void Append(ReadOnlySpan<byte> source)
    {
        foreach (byte data in source)
        {
            _state ^= data;
            _state *= Prime;
        }
    }

    public override void Reset()
    {
        _state = OffsetBasis;
    }

    protected override void GetCurrentHashCore(Span<byte> destination)
    {
        BitConverter.TryWriteBytes(destination, _state);
    }
}