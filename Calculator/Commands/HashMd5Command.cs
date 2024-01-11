using System.Security.Cryptography;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class HashMd5Command : HashCommandBase
{
    public HashMd5Command(IHost host) : base(host, MD5.Create())
    {
    }

    public override string[] Names => ["md5"];

    public override string Synopsys 
        => "Computes the MD5 hash of a file";
}