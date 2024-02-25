//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Internal;
using Calculator.Resources;

using CalculatorShell.Core;

using Markdig;

namespace Calculator.AutoRun;

internal sealed class ManualRenderAutoExec : IAutoExec
{
    public string LogMessage => "Checking manual existance";

    public int Priority => 100;

    private static bool IsNewer(string file, DateTime programDate)
    {
        if (!File.Exists(file))
        {
            return false;
        }

        var fileDate = File.GetCreationTime(file);

        return programDate > fileDate;
    }

    private static DateTime GetProgramDate()
    {
        Version v = Helpers.GetAssemblyVersion();
        return new DateTime(v.Major, v.Minor, v.Build);
    }

    public void Execute(IHost host)
    {
        string manualFile = Path.Combine(AppContext.BaseDirectory, "man.html");

        DateTime programDate = GetProgramDate();

        if (File.Exists(manualFile) &&
            !IsNewer(manualFile, programDate))
        {
            return;
        }

        host.Log.Info($"Updating manual to latest version...");

        string template = Helpers.GetResourceString(ResourceNames.TemplateHtml);

        var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

        string rendered = Markdown.ToHtml(Helpers.GetResourceString(ResourceNames.ManualMd), pipeline);

        string final = template.Replace("<!--{content}-->", rendered);

        File.WriteAllText(manualFile, final);
        File.SetCreationTime(manualFile, programDate);
    }
}
