//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;
using System.Net.Mime;
using System.Text;

using Calculator.Internal;
using Calculator.Messages;
using Calculator.Resources;
using Calculator.Web.Server;

using CalculatorShell.Core;

namespace Calculator.RequestHandlers;

internal sealed class ManualRequestHandler : IRequestHandler
{
    private readonly HashSet<string> _urls;
    private readonly string _menu;

    public ManualRequestHandler(IHost host)
    {
        var commands = host.Mediator
            .Request<IEnumerable<IGrouping<string, (string, string[])>>, CommandList>(new CommandList())
            ?? throw new CommandException("There are no available commands");
        
        _urls = commands
            .SelectMany(x => x)
            .SelectMany(x => x.Item2)
            .Select(x => $"/man-{x}.html")
            .ToHashSet();

        _urls.Add("/manual.html");

        _menu = GenerateCommandMenu(commands);
    }

    private static string GenerateCommandMenu(IEnumerable<IGrouping<string, (string, string[])>> commands)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var group in commands)
        {
            var categoryName = group.Key;
            var commandNames = group.SelectMany(g => g.Item2);
            sb.AppendLine($"<li><a href=\"#\">{categoryName}</a>");
            sb.AppendLine("  <ul>");
            foreach (var command in commandNames)
            {
                sb.AppendLine($"    <li><a href=\"/man-{command}.html\">{command}</a></li>");
            }
            sb.AppendLine("  </ul>");
            sb.AppendLine("</li>");
        }
        return sb.ToString();
    }

    public bool HandleRequest(HttpListenerContext context)
    {
        var url = _urls.FirstOrDefault(url => context.IsMatch("GET", url));
        if (string.IsNullOrEmpty(url))
            return false;

        string title = "Manual";

        var tempate = new Template(Helpers.GetResourceString(ResourceNames.ManualHtml));
        tempate.ApplyTag("menu", _menu);
        tempate.ApplyTag(Template.Title, title);


        context.Transfer(tempate.Render(), MediaTypeNames.Text.Html, HttpStatusCode.OK);
        return true;
    }
}
