using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Markdig;

namespace Calculator.Man;
internal class Man
{
    private static string GetResource(string name)
    {
        using (var stream = typeof(Man).Assembly.GetManifestResourceStream(name))
        {
            if (stream == null)
                return string.Empty;

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    public static void RenderMan()
    {
        string manualFile = Path.Combine(AppContext.BaseDirectory, "man.html");

        DateTime programDate = GetProgramDate();

        if (File.Exists(manualFile) &&
            !IsNewer(manualFile, programDate))
        {
            return;
        }

        string template = GetResource("Calculator.Man.Template.html");

        var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

        string rendered = Markdown.ToHtml(GetResource("Calculator.manual.md"), pipeline);

        string final = template.Replace("<!--{content}-->", rendered);

        File.WriteAllText(manualFile, final);
        File.SetCreationTime(manualFile, programDate);
    }

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
        Version v = typeof(Man).Assembly.GetName().Version
            ?? new Version();

        return new DateTime(v.Major, v.Minor, v.Build);
    }
}
