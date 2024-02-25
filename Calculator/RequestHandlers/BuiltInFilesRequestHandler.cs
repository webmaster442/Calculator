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

internal sealed class BuiltInFilesRequestHandler : IRequestHandler
{
    public bool HandleRequest(HttpListenerContext context)
    {
        if (context.IsMatch("GET", "/favicon.ico"))
            return Serve(context, ResourceNames.Icon, MediaTypeNames.Image.Icon);

        else if (context.IsMatch("GET", "/uikit.min.css"))
            return Serve(context, ResourceNames.UiKitCss, MediaTypeNames.Text.Css);

        else if (context.IsMatch("GET", "/uikit.min.js"))
            return Serve(context, ResourceNames.UiKitJs, MediaTypeNames.Text.JavaScript);

        else if (context.IsMatch("GET", "/uikit-icons.min.js"))
            return Serve(context, ResourceNames.UiKitIconJs, MediaTypeNames.Text.JavaScript);

        return false;
    }

    private static bool Serve(HttpListenerContext context, string resourceName, string mime)
    {
        using var iconStream = Helpers.GetResourceStream(resourceName);
        context.Tranfer(iconStream, mime, HttpStatusCode.OK);
        return true;
    }
}
