using System.Security.Cryptography;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha1Command : HashCommandBase
{
    public HashSha1Command(IHost host) : base(host, SHA1.Create())
    {
    }

    public override string[] Names => ["sha-1"];

    public override string Synopsys
        => "Computes the SHA-1 hash of a file";
}
