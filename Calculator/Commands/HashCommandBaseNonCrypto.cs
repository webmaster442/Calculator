//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.IO.Hashing;

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents.Hashing;

namespace Calculator.Commands;

internal abstract class HashCommandBaseNonCrypto : HashCommandbase
{
    private readonly NonCryptographicHashAlgorithm _algorithm;

    public HashCommandBaseNonCrypto(IHost host, NonCryptographicHashAlgorithm algorithm) : base(host)
    {
        _algorithm = algorithm;
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