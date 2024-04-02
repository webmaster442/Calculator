//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.IO.Hashing;
using System.Security.Cryptography;

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha1Command : HashCommandBaseCrypto
{
    public HashSha1Command(IHost host) : base(host, SHA1.Create())
    {
    }

    public override IArgumentCompleter? ArgumentCompleter
        => new FileNameCompleter(Host);

    public override string[] Names => ["sha-1"];

    public override string Synopsys
        => "Computes the SHA-1 hash of a file or string";
}

internal sealed class HashCrc64Command : HashCommandBaseNonCrypto
{
    public HashCrc64Command(IHost host) : base(host, new Crc64())
    {
    }

    public override string[] Names => ["crc-64"];

    public override string Synopsys
        => "Computes the CRC-64 hash of a file or string";
}
