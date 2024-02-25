//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;
using System.Net.Mime;
using System.Text;

namespace Calculator.Web.Server;

internal sealed class ExceptionRequestHandler : IRequestHandler
{
    public Exception? Exeption { get; internal set; }

    public bool HandleRequest(HttpListenerContext context)
    {
        StringBuilder message = new();
        message.AppendLine("500 internal server error");
        message.AppendLine(Exeption?.Message);
#if DEBUG
        message.AppendLine("--------------------------------------");
        message.AppendLine("Stack trace:");
        message.AppendLine(Exeption?.StackTrace);
#endif

        context.Transfer(message, MediaTypeNames.Text.Plain, HttpStatusCode.InternalServerError);

        return true;
    }
}
