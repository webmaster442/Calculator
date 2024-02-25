//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;
using System.Net.Mime;

using Calculator.Internal;
using Calculator.Resources;
using Calculator.Web.Server;

using Markdig;

namespace Calculator.HttpUi;

internal class ManualRequestHandler : IRequestHandler
{
    private readonly string _content;

    public ManualRequestHandler()
    {
        string template = Helpers.GetResourceString(ResourceNames.TemplateHtml);
        var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        string rendered = Markdown.ToHtml(Helpers.GetResourceString(ResourceNames.ManualMd), pipeline);
        _content = template.Replace("<!--{content}-->", rendered);
    }

    public bool HandleRequest(HttpListenerContext context)
    {
        if (!context.IsMatch("GET", "/manual.html"))
            return false;

        context.Transfer(_content, MediaTypeNames.Text.Html, HttpStatusCode.OK);

        return true;
    }
}
