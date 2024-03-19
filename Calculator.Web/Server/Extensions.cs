//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;
using System.Text;

namespace Calculator.Web.Server;

public static class Extensions
{
    public static void Transfer(this HttpListenerContext context,
                                string content,
                                string mimetype,
                                HttpStatusCode code)
    {
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = mimetype;
        context.Response.SendChunked = true;
        using (var writer = new StreamWriter(context.Response.OutputStream))
        {
            writer.Write(content);
        }
    }

    public static void Transfer(this HttpListenerContext context,
                                StringBuilder content,
                                string mimetype,
                                HttpStatusCode code)
    {
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = mimetype;
        context.Response.SendChunked = true;
        using (var writer = new StreamWriter(context.Response.OutputStream))
        {
            writer.Write(content.ToString());
        }
    }

    public static void Tranfer(this HttpListenerContext context,
                               Stream content,
                               string mimetype,
                               HttpStatusCode code)
    {
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = mimetype;
        context.Response.SendChunked = false;
        context.Response.ContentLength64 = content.Length;
        content.CopyTo(context.Response.OutputStream);
    }

    public static string ToLogMessage(this HttpListenerContext context)
    {
        return $"{context.Request.HttpMethod} {context.Request.Url}";
    }

    public static bool IsMatch(this HttpListenerContext context, string method, string url)
    {
        return context.Request.HttpMethod == method
            && context.Request.Url != null
            && context.Request.Url.LocalPath == url;
    }

    public static IReadOnlyDictionary<string, string> GetQueryParameters(this Uri uri)
    {
        var keyvalues = uri.Query.Split(new char[] { '&', '?' }, StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, string> results = new();
        foreach (var keyvalue in keyvalues)
        {
            var parts = keyvalue.Split('=');
            if (parts.Length == 2)
            {
                results.Add(parts[0], parts[1]);
            }
        }
        return results;
    }
}
