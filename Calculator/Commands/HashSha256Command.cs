using System.Security.Cryptography;

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha256Command : HashCommandBase
{
    public HashSha256Command(IHost host) : base(host, SHA256.Create())
    {
    }

    public override IArgumentCompleter? ArgumentCompleter
        => new FileNameCompleter(Host);

    public override string[] Names => ["sha-256"];

    public override string Synopsys
        => "Computes the SHA-256 hash of a file";
}
