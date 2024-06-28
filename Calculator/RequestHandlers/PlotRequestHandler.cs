using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Calculator.Internal;
using Calculator.Resources;
using Calculator.Web.Server;

using OxyPlot;

namespace Calculator.RequestHandlers;
internal class PlotRequestHandler : HtmlRequestHandler
{
    private readonly PlotModel _plotModel;
    

    public PlotRequestHandler() 
        : base(Helpers.GetResourceString(ResourceNames.PlotHtml), "/plot.html", cancache: false)
    {
        _plotModel = new PlotModel
        {
            Background = OxyColor.FromRgb(255, 255, 255),
            Culture = CultureInfo.InvariantCulture
        };
    }

    protected override string RenderContent(Template template, Parameters parameters)
    {
        return template
            .ApplyTag(Template.Title, "Plot")
            .ApplyTag(Template.Content, "p")
            .Render();
    }
}
