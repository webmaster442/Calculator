using System.Security.Cryptography;

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashSha384Command : HashCommandBase
{
    public HashSha384Command(IHost host) : base(host, SHA384.Create())
    {
    }

    public override IArgumentCompleter? ArgumentCompleter
        => new FileNameCompleter(Host.Log);

    public override string[] Names => ["sha-384"];

    public override string Synopsys
        => "Computes the SHA-384 hash of a file";
}
