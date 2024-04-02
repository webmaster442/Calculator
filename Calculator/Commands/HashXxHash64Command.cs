//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.IO.Hashing;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashXxHash64Command : HashCommandBaseNonCrypto
{
    public HashXxHash64Command(IHost host) : base(host, new XxHash64())
    {
    }

    public override string[] Names => ["xxHash64"];

    public override string Synopsys
        => "Computes the XxHash64 hash of a file or string";
}
