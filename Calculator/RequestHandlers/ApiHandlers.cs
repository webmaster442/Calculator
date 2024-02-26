//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;

using Calculator.Web.Server;
using Calculator.Web.Server.Api;

using CalculatorShell.Core;

namespace Calculator.RequestHandlers;
internal class ApiHandlers : ApiRequestHandler<ApiHandlers>
{
    public ApiHandlers(IHost host) : base(host.Log)
    {
    }

    [GetRoute("/api/clearlog")]
    public void ClearLog(HttpListenerContext context)
    {
        var parameters = context.Request.Url?.GetQueryParameters();
        if (parameters != null
            && parameters.ContainsKey("confirmed")
            && bool.TryParse(parameters["confirmed"], out bool confirmValue)
            && confirmValue)
        {
            Log.Clear();
        }
        context.Response.Redirect("/log.html");
        context.Response.Close();
    }
}
