//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Markdig;

namespace Calculator.RequestHandlers;

internal static class Extensions
{
    private static readonly MarkdownPipeline Pipeline
        = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

    public static string MakdownToHtml(this string markdown)
    {
        return Markdown.ToHtml(markdown, Pipeline);
    }
}
