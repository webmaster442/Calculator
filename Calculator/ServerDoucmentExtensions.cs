//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics;

using CalculatorShell.Core;

namespace Calculator;
internal static class ServerDoucmentExtensions
{
    public static string ToUrlString(this ServerDocument document, int port)
    {
        return document switch
        {
            ServerDocument.Manual => $"http://localhost:{port}/manual.html",
            ServerDocument.Log => $"http://localhost:{port}/log.html",
            ServerDocument.Plot => $"http://localhost:{port}/plot.html",
            _ => throw new UnreachableException(),
        };
    }
}
