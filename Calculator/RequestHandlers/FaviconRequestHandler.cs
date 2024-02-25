//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;
using System.Net.Mime;

using Calculator.Internal;
using Calculator.Resources;
using Calculator.Web.Server;

namespace Calculator.RequestHandlers;

internal sealed class FaviconRequestHandler : IRequestHandler
{
    public bool HandleRequest(HttpListenerContext context)
    {
        if (!context.IsMatch("GET", "/favicon.ico"))
            return false;

        using var iconStream = Helpers.GetResourceStream(ResourceNames.Icon);
        context.Tranfer(iconStream, MediaTypeNames.Image.Icon, HttpStatusCode.OK);

        return true;
    }
}
