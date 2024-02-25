//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;

namespace Calculator.Web.Server;

public interface IRequestHandler
{
    bool HandleRequest(HttpListenerContext context);
}