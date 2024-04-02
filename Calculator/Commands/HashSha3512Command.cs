//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Security.Cryptography;

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha3512Command : HashCommandBaseCrypto, IPlatformSupportCheck
{
    public HashSha3512Command(IHost host) : base(host, SHA3_512.Create())
    {
    }

    public override IArgumentCompleter? ArgumentCompleter
        => new FileNameCompleter(Host);

    public override string[] Names => ["sha3-512"];

    public override string Synopsys
        => "Computes the SHA3-512 hash of a file or string";

    public static bool IsSupported()
        => SHA3_512.IsSupported;
}