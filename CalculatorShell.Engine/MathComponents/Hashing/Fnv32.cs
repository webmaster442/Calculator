//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.IO.Hashing;

namespace CalculatorShell.Engine.MathComponents.Hashing;

public sealed class Fnv32 : NonCryptographicHashAlgorithm
{
    private const uint OffsetBasis = 2166136261;
    private const uint Prime = 16777619;

    private uint _state;

    public Fnv32() : base(sizeof(uint))
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
