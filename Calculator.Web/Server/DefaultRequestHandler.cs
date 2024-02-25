//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;
using System.Net.Mime;

namespace Calculator.Web.Server;

internal sealed class DefaultRequestHandler : IRequestHandler
{
    public bool HandleRequest(HttpListenerContext context)
    {
        var message = $"No handler configured for {context.Request.HttpMethod} {context.Request.RawUrl}";

        context.Transfer(message, MediaTypeNames.Text.Plain, HttpStatusCode.NotFound);

        return true;
    }
}
