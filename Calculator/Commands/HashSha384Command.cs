using System.Security.Cryptography;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha384Command : HashCommandBase
{
    public HashSha384Command(IHost host) : base(host, SHA384.Create())
    {
    }

    public override string[] Names => ["sha-384"];

    public override string Synopsys
        => "Computes the SHA-384 hash of a file";
}
