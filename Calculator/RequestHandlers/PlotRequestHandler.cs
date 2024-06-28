//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Internal;
using Calculator.Messages;
using Calculator.Resources;
using Calculator.Web.Server;

using CalculatorShell.Core;

namespace Calculator.RequestHandlers;
internal class PlotRequestHandler : HtmlRequestHandler
{
    private readonly PlotRendering _plotRendering;

    public PlotRequestHandler(IHost host) 
        : base(Helpers.GetResourceString(ResourceNames.PlotHtml), "/plot.html", cancache: false)
    {
        _plotRendering = new PlotRendering(host.Mediator);
    }

    protected override string RenderContent(Template template, Parameters parameters)
    {
        if (parameters.Post.Count > 0)
        {
            var inputs = parameters.MapPost<PlotRenderInput>();
            if (!inputs.TryValidate(out var issues))
            {
                var errorText = string.Join("<br>\r\n", issues.Select(x => x.ErrorMessage));
                return template.ApplyTag(Template.Content, errorText).Render();
            }
            return template.ApplyTag(Template.Content, _plotRendering.Render(inputs)).Render();
        }
        else
        {
            return template.ApplyTag(Template.Content, string.Empty).Render();
        }

    }
}
