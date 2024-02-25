//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics;

using CalculatorShell.Core;

namespace Calculator;
internal static class ServerDoucmentExtensions
{
    public static string ToUrlString(this ServerDocument document)
    {
        return document switch
        {
            ServerDocument.Manual => "http://localhost:11111/manual.html",
            ServerDocument.Log => "http://localhost:11111/log.html",
            _ => throw new UnreachableException(),
        };
    }
}
