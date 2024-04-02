//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Security.Cryptography;

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

using CommandLine;

namespace Calculator.Commands;
internal abstract class CryptoHashCommandBase : ShellCommandAsync, IDisposable, IProgress<long>
{
    private readonly HashAlgorithm _algorithm;
    private bool _disposed;

    protected CryptoHashCommandBase(IHost host, HashAlgorithm algorithm) : base(host)
    {
        _algorithm = algorithm;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _algorithm.Dispose();
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~CryptoHashCommandBase()
    {
        Dispose(false);
    }

    public override string Category
        => CommandCategories.Hash;

    internal sealed class HashOptions
    {
        [Value(0, HelpText = "File name or string to hash")]
        public string Data { get; set; }

        [Option('s', "string", HelpText = "Treats input as string, istead of file")]
        public bool IsString { get; set; }

        public HashOptions()
        {
            Data = string.Empty;
        }
    }

    public override string HelpMessage
        => this.BuildHelpMessage<HashOptions>();

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        HashCalculator hc = new(this);

        var options = args.Parse<HashOptions>(Host);


        if (options.IsString)
        {
            if (string.IsNullOrEmpty(options.Data))
            {
                throw new CommandException("No input is specified");
            }

            HashResult stringResult = hc.ComputeHash(_algorithm, options.Data);
            Host.Output.Result(stringResult);
            return;
        }

        Stream stream = null!;
        try
        {
            if (!string.IsNullOrEmpty(options.Data))
            {
                stream = File.OpenRead(options.Data);
            }
            else
            {
                var name = await Host.Dialogs.SelectFile(cancellationToken);
                stream = File.OpenRead(name);
            }
            HashResult result = await hc.ComputeHashAsync(_algorithm, stream, cancellationToken);
            Host.Output.Result(result);
        }
        finally
        {
            stream.Dispose();
        }
    }

    public virtual void Report(long value)
    {
        Host.Output.Markup($"Processed: {value} bytes\r");
    }
}
