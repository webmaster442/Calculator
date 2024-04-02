//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Security.Cryptography;

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha512Command : CryptoHashCommandBase
{
    public HashSha512Command(IHost host) : base(host, SHA512.Create())
    {
    }

    public override IArgumentCompleter? ArgumentCompleter
        => new FileNameCompleter(Host);

    public override string[] Names => ["sha-512"];

    public override string Synopsys
        => "Computes the SHA-512 hash of a file";
}
