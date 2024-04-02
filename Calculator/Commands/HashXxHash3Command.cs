﻿//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.IO.Hashing;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashXxHash3Command : HashCommandBaseNonCrypto
{
    public HashXxHash3Command(IHost host) : base(host, new XxHash3())
    {
    }

    public override string[] Names => ["xxHash3"];

    public override string Synopsys
        => "Computes the XxHash3 hash of a file or string";
}
