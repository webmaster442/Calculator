//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;

using HttpMultipartParser;

namespace Calculator.Web.Server;

public sealed class Parameters
{
    public IReadOnlyDictionary<string, string> Get { get; }
    public IReadOnlyDictionary<string, string> Post { get; }

    private Parameters(IReadOnlyDictionary<string, string> get, IReadOnlyDictionary<string, string> post)
    {
        Get = get;
        Post = post;
    }

    public static Parameters Create(HttpListenerContext context)
    {
        if (context.Request.HttpMethod == "GET")
            return FromGet(context);
        else if (context.Request.HttpMethod == "POST")
            return FromPost(context);
        else
            return new Parameters(new Dictionary<string, string>(), new Dictionary<string, string>());
    }

    private static Parameters FromGet(HttpListenerContext context)
    {
        Dictionary<string, string> get = new();
        foreach (var key in context.Request.QueryString.AllKeys)
        {
            if (key != null)
                get.Add(key, context.Request.QueryString[key] ?? string.Empty);
        }
        return new Parameters(get, new Dictionary<string, string>());
    }

    private static Parameters FromPost(HttpListenerContext context)
    {
        Dictionary<string, string> post = new();
        if (context.Request.ContentType?.StartsWith("multipart/form-data") == false)
        {
            return new Parameters(new Dictionary<string, string>(), post);
        }
        var parsed = MultipartFormDataParser.Parse(context.Request.InputStream);
        foreach (var parameter in parsed.Parameters)
        {
            post.Add(parameter.Name, parameter.Data);
        }
        return new Parameters(new Dictionary<string, string>(), post);
    }
}
