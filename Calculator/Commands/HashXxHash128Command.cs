//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.IO.Hashing;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashXxHash128Command : HashCommandBaseNonCrypto
{
    public HashXxHash128Command(IHost host) : base(host, new XxHash128())
    {
    }

    public override string[] Names => ["xxHash128"];

    public override string Synopsys
        => "Computes the XxHash128 hash of a file or string";
}