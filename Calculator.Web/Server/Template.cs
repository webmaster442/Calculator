using System.Text;

namespace Calculator.Web.Server;

public sealed class Template
{
    private readonly StringBuilder _stringBuilder;

    public Template(string content)
    {
        _stringBuilder = new StringBuilder(content, 8192);
    }

    public Template ApplyTag(string tag, string value)
    {
        _stringBuilder.Replace($"<!--{{{tag}}}-->", value);
        return this;
    }

    public string Render()
    {
        return _stringBuilder.ToString();
    }

    public const string Content = "content";
    public const string Title = "title";
}