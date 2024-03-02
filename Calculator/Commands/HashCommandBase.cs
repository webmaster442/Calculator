//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Security.Cryptography;

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

using CommandLine;

namespace Calculator.Commands;
internal abstract class HashCommandBase : ShellCommandAsync, IDisposable, IProgress<long>
{
    private readonly HashAlgorithm _algorithm;
    private bool _disposed;

    protected HashCommandBase(IHost host, HashAlgorithm algorithm) : base(host)
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

    ~HashCommandBase()
    {
        Dispose(false);
    }

    internal class HashOptions
    {
        [Value(0, HelpText = "File name")]
        public string FileName { get; set; }

        public HashOptions()
        {
            FileName = string.Empty;
        }
    }

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        HashCalculator hc = new(this);

        var options = args.Parse<HashOptions>(Host);

        Stream stream = null!;
        try
        {
            if (!string.IsNullOrEmpty(options.FileName))
            {
                stream = File.OpenRead(options.FileName);
            }
            else
            {
                var name = await Host.Dialogs.SelectFile(cancellationToken);
                stream = File.OpenRead(name);
            }
            var result = await hc.ComputeHashAsync(_algorithm, stream, cancellationToken);
            Host.Output.Result(result.ToString());
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
