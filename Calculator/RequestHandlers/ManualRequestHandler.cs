//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Internal;
using Calculator.Resources;

using Markdig;

namespace Calculator.RequestHandlers;

internal sealed class ManualRequestHandler : HtmlRequestHandler
{
    public ManualRequestHandler()
        : base(Helpers.GetResourceString(ResourceNames.TemplateHtml), "/manual.html", cancache: true)
    {
    }

    protected override string RenderContent(Template template)
    {
        var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        string rendered = Markdown.ToHtml(Helpers.GetResourceString(ResourceNames.ManualMd), pipeline);

        return template
            .ApplyTag(Template.Title, "Calculator Shell manual")
            .ApplyTag(Template.Content, rendered)
            .Render();
    }
}
