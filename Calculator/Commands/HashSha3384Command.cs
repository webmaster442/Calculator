using System.Security.Cryptography;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha3384Command : HashCommandBase
{
    public HashSha3384Command(IHost host) : base(host, SHA3_384.Create())
    {
    }

    public override string[] Names => ["sha3-384"];

    public override string Synopsys
        => "Computes the SHA3-384 hash of a file";
}
