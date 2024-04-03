//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents.Hashing;

namespace Calculator.Commands;

internal sealed class HashFnv32Command : HashCommandBaseNonCrypto
{
    public HashFnv32Command(IHost host) : base(host, new Fnv32())
    {
    }

    public override string[] Names => ["Fnv32"];

    public override string Synopsys
        => "Computes the Fnv32 hash of a file or string";
}
