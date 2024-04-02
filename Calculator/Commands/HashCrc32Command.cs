//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.IO.Hashing;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashCrc32Command : HashCommandBaseNonCrypto
{
    public HashCrc32Command(IHost host) : base(host, new Crc32())
    {
    }

    public override string[] Names => ["crc-32"];

    public override string Synopsys 
        => "Computes the CRC-32 hash of a file or string";
}
