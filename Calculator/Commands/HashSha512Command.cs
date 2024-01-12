using System.Security.Cryptography;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha512Command : HashCommandBase
{
    public HashSha512Command(IHost host) : base(host, SHA512.Create())
    {
    }

    public override string[] Names => ["sha-512"];

    public override string Synopsys
        => "Computes the SHA-512 hash of a file";
}
