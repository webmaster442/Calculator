﻿//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Security.Cryptography;

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents.Hashing;

namespace Calculator.Commands;

internal abstract class HashCommandBaseCrypto : HashCommandbase, IDisposable
{
    private readonly HashAlgorithm _algorithm;
    private bool _disposed;

    protected HashCommandBaseCrypto(IHost host, HashAlgorithm algorithm) : base(host)
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

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        HashCalculator calculator = new(this);
        var options = args.Parse<HashOptions>(Host);

        if (options.IsString)
        {
            if (string.IsNullOrEmpty(options.Data))
            {
                throw new CommandException("No input is specified");
            }

            HashResult stringResult = calculator.ComputeHash(_algorithm, options.Data);
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
            HashResult result = await calculator.ComputeHashAsync(_algorithm, stream, cancellationToken);
            Host.Output.Result(result);
        }
        finally
        {
            stream.Dispose();
        }
    }
}
