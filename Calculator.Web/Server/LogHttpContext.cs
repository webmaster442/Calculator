using System.Collections.Specialized;
using System.Net;

namespace Calculator.Web.Server;
public class LogHttpContext
{
    public LogHttpContext(HttpListenerContext context)
    {
        ContentType = context.Request.ContentType;
        HttpMethod = context.Request.HttpMethod;
        Headers = context.Request.Headers;
        RawUrl = context.Request.RawUrl;
    }

    public string? ContentType { get; }
    public string HttpMethod { get; }
    public NameValueCollection Headers { get; }
    public string? RawUrl { get; }
}
