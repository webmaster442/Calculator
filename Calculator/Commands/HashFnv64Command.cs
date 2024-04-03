//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents.Hashing;

namespace Calculator.Commands;

internal sealed class HashFnv64Command : HashCommandBaseNonCrypto
{
    public HashFnv64Command(IHost host) : base(host, new Fnv64())
    {
    }

    public override string[] Names => ["Fnv64"];

    public override string Synopsys
        => "Computes the Fnv64 hash of a file or string";
}
