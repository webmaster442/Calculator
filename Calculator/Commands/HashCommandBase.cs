//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Security.Cryptography;

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

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

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        HashCalculator hc = new(this);

        Stream stream = null!;
        try
        {
            if (args.Length == 1)
            {
                stream = File.OpenRead(args[0]);
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
