//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Security.Cryptography;

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashMd5Command : CryptoHashCommandBase
{
    public HashMd5Command(IHost host) : base(host, MD5.Create())
    {
    }

    public override IArgumentCompleter? ArgumentCompleter
        => new FileNameCompleter(Host);

    public override string[] Names => ["md5"];

    public override string Synopsys
        => "Computes the MD5 hash of a file";
}
