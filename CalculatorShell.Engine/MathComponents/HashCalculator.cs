//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Security.Cryptography;

namespace CalculatorShell.Engine.MathComponents;

public class HashCalculator
{
    private readonly IProgress<long> _progressReporter;

    private const long reportSize = 1024 * 1024;

    public HashCalculator(IProgress<long> progressReporter)
    {
        _progressReporter = progressReporter;
    }

    public async Task<HashResult> ComputeHashAsync(HashAlgorithm hashAlgorithm,
                                                   Stream stream,
                                                   CancellationToken cancellationToken)
    {
        byte[] buffer;
        int readAheadBytesRead, bytesRead;
        long totalBytesRead = 0;
        long lastReported = 0;
        byte[] readAheadBuffer = new byte[4096];

        readAheadBytesRead = await stream.ReadAsync(readAheadBuffer, cancellationToken);
        totalBytesRead += readAheadBytesRead;
        do
        {
            bytesRead = readAheadBytesRead;
            buffer = readAheadBuffer;
            readAheadBuffer = new byte[4096];
            readAheadBytesRead = await stream.ReadAsync(readAheadBuffer, cancellationToken);
            totalBytesRead += readAheadBytesRead;

            if (readAheadBytesRead == 0)
            {
                hashAlgorithm.TransformFinalBlock(buffer, 0, bytesRead);
            }
            else
            {
                hashAlgorithm.TransformBlock(buffer, 0, bytesRead, buffer, 0);
            }

            if (totalBytesRead > lastReported + reportSize)
            {
                _progressReporter.Report(totalBytesRead);
                lastReported = totalBytesRead;
            }
            cancellationToken.ThrowIfCancellationRequested();

        }
        while (readAheadBytesRead != 0);

        if (hashAlgorithm.Hash == null)
            throw new InvalidOperationException();

        return new HashResult(hashAlgorithm.Hash);
    }
}
