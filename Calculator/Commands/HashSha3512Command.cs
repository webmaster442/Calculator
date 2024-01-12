using System.Security.Cryptography;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha3512Command : HashCommandBase
{
    public HashSha3512Command(IHost host) : base(host, SHA3_512.Create())
    {
    }

    public override string[] Names => ["sha3-512"];

    public override string Synopsys
        => "Computes the SHA3-512 hash of a file";
}