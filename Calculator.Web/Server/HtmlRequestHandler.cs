//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;
using System.Net.Mime;
using System.Text;

namespace Calculator.Web.Server;

public abstract class HtmlRequestHandler : IRequestHandler
{
    private readonly string _templateContent;
    private readonly string _url;
    private readonly bool _cancache;
    private string? _cache;

    public sealed class Template
    {
        private readonly StringBuilder _stringBuilder;

        public Template(string content)
        {
            _stringBuilder = new StringBuilder(content, 8192);
        }

        public Template ApplyTag(string tag, string value)
        {
            _stringBuilder.Replace($"<!--{{{tag}}}-->", value);
            return this;
        }

        public string Render()
        {
            return _stringBuilder.ToString();
        }

        public const string Content = "content";
        public const string Title = "title";
    }

    protected HtmlRequestHandler(string templateContent, string url, bool cancache)
    {
        _templateContent = templateContent;
        _url = url;
        _cancache = cancache;
    }

    public bool HandleRequest(HttpListenerContext context)
    {
        if (!context.IsMatch("GET", _url))
            return false;

        

        if (_cancache)
        {
            if (_cache == null)
            {
                var template = new Template(_templateContent);
                _cache = RenderContent(template);
            }
            context.Transfer(_cache, MediaTypeNames.Text.Html, HttpStatusCode.OK);
            return true;
        }

        var nonCachedTemplate = new Template(_templateContent);
        context.Response.Headers.Add(HttpResponseHeader.CacheControl, "no-store");
        var content = RenderContent(nonCachedTemplate);
        context.Transfer(content, MediaTypeNames.Text.Html, HttpStatusCode.OK);
        return true;
    }

    protected abstract string RenderContent(Template template);
}
