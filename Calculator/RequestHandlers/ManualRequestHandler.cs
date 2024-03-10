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
    private readonly Dictionary<string, string> _urls;
    private readonly string _menu;
    private readonly IHost _host;
    private readonly Dictionary<string, string> _statics;

    public ManualRequestHandler(IHost host)
    {
        var commandsByCategory = host.Mediator
            .Request<IDictionary<string, HashSet<string>>, CommandList>(new CommandList())
            ?? throw new InvalidOperationException("There are no available commands");

        _urls = commandsByCategory.Values
            .SelectMany(x => x)
            .ToDictionary(x => $"/man-{x}.html", x => x);

        _urls.Add("/manual.html", string.Empty);

        _menu = GenerateCommandMenu(commandsByCategory);
        _host = host;

        _statics = new Dictionary<string, string>
        {
            { "/man-colors.html", Helpers.GetResourceString(ResourceNames.ManColors).MakdownToHtml() },
            { "/man-constants.html", Helpers.GetResourceString(ResourceNames.ManConstants).MakdownToHtml() },
            { "/man-functionnames.html", Helpers.GetResourceString(ResourceNames.ManFunctions).MakdownToHtml() },
            { "/man-numberformats.html", Helpers.GetResourceString(ResourceNames.ManNumberformats).MakdownToHtml() },
        };
    }

    private static string GenerateCommandMenu(IDictionary<string, HashSet<string>> commandsByCategory)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var category in commandsByCategory)
        {
            var categoryName = category.Key;
            var commandNames = category.Value;
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

    private string RenderHelp(string commandName)
    {
        string hlp = _host.Mediator.Request<string, HelpRequestMessage>(new HelpRequestMessage(commandName))
            ?? throw new InvalidOperationException("There is no available help");

        return $"""
                <h1 class=\"title\">{commandName}</h1>
                {hlp.MakdownToHtml()}
                """;
    }

    public bool HandleRequest(HttpListenerContext context)
    {
        //entry => { url, command name }
        var entry = _urls.FirstOrDefault(entry => context.IsMatch("GET", entry.Key));

        if (string.IsNullOrEmpty(entry.Key))
        {
            var staticFile = _statics.FirstOrDefault(entry => context.IsMatch("GET", entry.Key));
            if (!string.IsNullOrEmpty(staticFile.Key))
            {
                return RenderStaticFile(context, staticFile);
            }
            return false;
        }

        string title = "Manual";

        if (!string.IsNullOrEmpty(entry.Value))
        {
            title = $"Manual - {entry.Value}";
        }

        var tempate = new Template(Helpers.GetResourceString(ResourceNames.ManualHtml));
        tempate.ApplyTag("menu", _menu);
        tempate.ApplyTag(Template.Title, title);

        if (!string.IsNullOrEmpty(entry.Value))
        {
            tempate.ApplyTag(Template.Content, RenderHelp(entry.Value));
        }


        context.Transfer(tempate.Render(), MediaTypeNames.Text.Html, HttpStatusCode.OK);
        return true;
    }

    private bool RenderStaticFile(HttpListenerContext context, KeyValuePair<string, string> staticFile)
    {
        var tempate = new Template(Helpers.GetResourceString(ResourceNames.ManualHtml));
        tempate.ApplyTag("menu", _menu);
        tempate.ApplyTag(Template.Title, staticFile.Key);
        tempate.ApplyTag(Template.Content, staticFile.Value);
        context.Transfer(tempate.Render(), MediaTypeNames.Text.Html, HttpStatusCode.OK);
        return true;

    }
}
