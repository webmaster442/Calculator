//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.IO.Hashing;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashXxHash32Command : HashCommandBaseNonCrypto
{
    public HashXxHash32Command(IHost host) : base(host, new XxHash32())
    {
    }

    public override string[] Names => ["xxHash32"];

    public override string Synopsys
        => "Computes the XxHash32 hash of a file or string";
}
