using System.Text;

using Calculator.Internal;
using Calculator.Resources;
using Calculator.Web.Server;

using CalculatorShell.Core;

namespace Calculator.RequestHandlers;
internal class LogRequestHandler : HtmlRequestHandler
{
    private readonly IHost _host;

    public LogRequestHandler(IHost host)
        : base(Helpers.GetResourceString(ResourceNames.TemplateHtml), "/log.html", cancache: false)
    {
        _host = host;
    }

    protected override string RenderContent(Template template)
    {
        var logstring = RenderLog();

        return template
            .ApplyTag(Template.Title, "Log")
            .ApplyTag(Template.Content, logstring)
            .Render();
    }

    private string RenderLog()
    {
        StringBuilder sb = new(4096);
        sb.AppendLine("<ol class=\"log\">");
        foreach (var item in _host.Log.Entries.OrderBy(x => x.Key))
        {
            if (item.Value.Contains(" Warning: "))
                sb.AppendLine($"<li class=\"monospaced yellow\">{item.Key} {item.Value}</li>");
            else if (item.Value.Contains(" Error: ") || item.Value.Contains(" Exception: "))
                sb.AppendLine($"<li class=\"monospaced red\">{item.Key} {item.Value}</li>");
            else
                sb.AppendLine($"<li>{item.Key} {item.Value}</li>");
        }
        sb.AppendLine("</ol>");
        return sb.ToString();
    }
}
