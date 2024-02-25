using System.Text;

using Calculator.Internal;
using Calculator.Resources;

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
        foreach (var item in _host.Log.Entries.OrderBy(x => x.Key))
        {
            if (item.Value.Contains(" Warning: "))
                sb.AppendLine($"<p class=\"monospaced yellow\"><i>{item.Key}</i> {item.Value}</p>");
            else if (item.Value.Contains(" Error: ") || item.Value.Contains(" Exception: "))
                sb.AppendLine($"<p class=\"monospaced red\"><i>{item.Key}</i> {item.Value}</p>");
            else
                sb.AppendLine($"<p><i>{item.Key}</i> {item.Value}</p>");
        }
        return sb.ToString();
    }
}
