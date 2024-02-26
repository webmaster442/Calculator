//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal sealed class HttpServerPort : PayloadBase
{
    public HttpServerPort(int port)
    {
        Port = port;
    }

    public int Port { get; }
}