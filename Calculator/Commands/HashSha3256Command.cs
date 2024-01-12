using System.Security.Cryptography;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha3256Command : HashCommandBase
{
    public HashSha3256Command(IHost host) : base(host, SHA3_256.Create())
    {
    }

    public override string[] Names => ["sha3-256"];

    public override string Synopsys
        => "Computes the SHA3-256 hash of a file";
}
