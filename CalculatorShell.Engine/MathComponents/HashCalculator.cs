//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.IO.Hashing;
using System.Security.Cryptography;
using System.Text;

namespace CalculatorShell.Engine.MathComponents;

public class HashCalculator
{
    private readonly IProgress<long> _progressReporter;

    private const long reportSize = 1024 * 1024;

    public HashCalculator(IProgress<long> progressReporter)
    {
        _progressReporter = progressReporter;
    }

    public HashResult ComputeHash(HashAlgorithm hashAlgorithm, string data)
    {
        byte[] hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(data));
        return new HashResult(hash);
    }

    public HashResult ComputeHash(NonCryptographicHashAlgorithm hashAlgorithm,
                                  string data)
    {
        hashAlgorithm.Append(Encoding.UTF8.GetBytes(data));
        return new HashResult(hashAlgorithm.GetHashAndReset());
    }

    public async Task<HashResult> ComputeHashAsync(NonCryptographicHashAlgorithm hashAlgorithm,
                                                   Stream stream,
                                                   CancellationToken cancellationToken)
    {
        byte[] buffer = new byte[4096];
        long totalBytesRead = 0;
        long lastReported = 0;
        int bytesRead;
        do
        {
            bytesRead = await stream.ReadAsync(buffer, cancellationToken);
            totalBytesRead += bytesRead;
            hashAlgorithm.Append(buffer.AsSpan(0..bytesRead));
            if (totalBytesRead > lastReported + reportSize)
            {
                _progressReporter.Report(totalBytesRead);
                lastReported = totalBytesRead;
            }
            cancellationToken.ThrowIfCancellationRequested();
        }
        while (bytesRead != 0);

        return new HashResult(hashAlgorithm.GetHashAndReset());
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
                _ = hashAlgorithm.TransformFinalBlock(buffer, 0, bytesRead);
            }
            else
            {
                _ = hashAlgorithm.TransformBlock(buffer, 0, bytesRead, buffer, 0);
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
