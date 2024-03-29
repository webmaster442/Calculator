//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

using Calculator.Internal;
using Calculator.Resources;
using Calculator.Web.Server;

using CalculatorShell.Core;

namespace Calculator.RequestHandlers;
internal sealed class LogRequestHandler : HtmlRequestHandler
{
    private readonly IHost _host;

    public LogRequestHandler(IHost host)
        : base(Helpers.GetResourceString(ResourceNames.LogHtml), "/log.html", cancache: false)
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
        _ = sb.AppendLine("<ol class=\"log\">");
        foreach (var item in _host.Log.Entries.OrderBy(x => x.Key))
        {
            if (item.Value.Contains(" Warning: "))
                _ = sb.AppendLine($"<li class=\"monospaced yellow\">{item.Key} {item.Value}</li>");
            else if (item.Value.Contains(" Error: ") || item.Value.Contains(" Exception: "))
                _ = sb.AppendLine($"<li class=\"monospaced red\">{item.Key} {item.Value}</li>");
            else
                _ = sb.AppendLine($"<li>{item.Key} {item.Value}</li>");
        }
        _ = sb.AppendLine("</ol>");
        return sb.ToString();
    }
}
