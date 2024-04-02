//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Security.Cryptography;

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha3256Command : HashCommandBaseCrypto, IPlatformSupportCheck
{
    public HashSha3256Command(IHost host) : base(host, SHA3_256.Create())
    {
    }

    public override IArgumentCompleter? ArgumentCompleter
        => new FileNameCompleter(Host);

    public override string[] Names => ["sha3-256"];

    public override string Synopsys
        => "Computes the SHA3-256 hash of a file or string";

    public static bool IsSupported()
        => SHA3_256.IsSupported;
}
