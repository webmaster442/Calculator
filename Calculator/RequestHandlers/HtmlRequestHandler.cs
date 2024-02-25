//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;
using System.Net.Mime;

using Calculator.Web.Server;

namespace Calculator.RequestHandlers;

internal abstract class HtmlRequestHandler : IRequestHandler
{
    private readonly string _templateContent;
    private readonly string _url;
    private readonly bool _cancache;
    private string? _cache;

    protected HtmlRequestHandler(string templateContent, string url, bool cancache)
    {
        _templateContent = templateContent;
        _url = url;
        _cancache = cancache;
    }

    public bool HandleRequest(HttpListenerContext context)
    {
        var template = new Template(_templateContent);

        if (!context.IsMatch("GET", _url))
            return false;

        if (_cancache)
        {
            _cache ??= RenderContent(template);
            context.Transfer(_cache, MediaTypeNames.Text.Html, HttpStatusCode.OK);
            return true;
        }

        context.Response.Headers.Add(HttpRequestHeader.CacheControl, "no-store");
        var content = RenderContent(template);
        context.Transfer(content, MediaTypeNames.Text.Html, HttpStatusCode.OK);
        return true;
    }

    protected abstract string RenderContent(Template template);
}
