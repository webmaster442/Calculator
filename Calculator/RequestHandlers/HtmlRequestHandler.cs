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
    private readonly string _url;
    private readonly bool _cancache;
    private readonly Template _template;
    private string? _cache;

    protected HtmlRequestHandler(string templateContent, string url, bool cancache)
    {
        _url = url;
        _cancache = cancache;
        _template = new Template(templateContent);
    }

    public bool HandleRequest(HttpListenerContext context)
    {
        if (!context.IsMatch("GET", _url))
            return false;

        if (_cancache)
        {
            _cache ??= RenderContent(_template);
            context.Transfer(_cache, MediaTypeNames.Text.Html, HttpStatusCode.OK);
            return true;
        }

        var content = RenderContent(_template);
        context.Transfer(content, MediaTypeNames.Text.Html, HttpStatusCode.OK);
        return true;
    }

    protected abstract string RenderContent(Template template);
}
