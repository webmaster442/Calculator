//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace Calculator.Web.Server;

public static class Extensions
{
    public static void Transfer(this HttpListenerContext context,
                                string content,
                                string mimetype,
                                HttpStatusCode code)
    {
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = mimetype;
        context.Response.SendChunked = true;
        using (var writer = new StreamWriter(context.Response.OutputStream))
        {
            writer.Write(content);
        }
    }

    public static void Transfer(this HttpListenerContext context,
                                StringBuilder content,
                                string mimetype,
                                HttpStatusCode code)
    {
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = mimetype;
        context.Response.SendChunked = true;
        using (var writer = new StreamWriter(context.Response.OutputStream))
        {
            writer.Write(content.ToString());
        }
    }

    public static void Tranfer(this HttpListenerContext context,
                               Stream content,
                               string mimetype,
                               HttpStatusCode code)
    {
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = mimetype;
        context.Response.SendChunked = false;
        context.Response.ContentLength64 = content.Length;
        content.CopyTo(context.Response.OutputStream);
    }

    public static LogHttpContext ToLog(this HttpListenerContext context)
    {
        return new LogHttpContext(context);
    }

    public static bool IsMatch(this HttpListenerContext context, string method, string url)
    {
        return context.Request.HttpMethod == method
            && context.Request.Url != null
            && context.Request.Url.LocalPath == url;
    }

    public static IReadOnlyDictionary<string, string> GetQueryParameters(this Uri uri)
    {
        var keyvalues = uri.Query.Split(new char[] { '&', '?' }, StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, string> results = new();
        foreach (var keyvalue in keyvalues)
        {
            var parts = keyvalue.Split('=');
            if (parts.Length == 2)
            {
                results.Add(parts[0], parts[1]);
            }
        }
        return results;
    }

    private static T Map<T>(IReadOnlyDictionary<string, string> args) where T : new()
    {
        var instance = new T();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (args.TryGetValue(property.Name, out string? value)
                && property.PropertyType.GetInterface(nameof(IConvertible)) != null)
            {
                try
                {
                    var mapped = Convert.ChangeType(value, property.PropertyType, CultureInfo.InvariantCulture);
                    property.SetValue(instance, mapped);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        return instance;
    }

    public static T MapGet<T>(this Parameters parameters) where T : new()
    {
        return Map<T>(parameters.Get);
    }

    public static T MapPost<T>(this Parameters parameters) where T : new()
    {
        return Map<T>(parameters.Post);
    }

    public static bool TryValidate(this object obj, out IReadOnlyList<ValidationResult> results)
    {
        var valiationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(obj, null, null);
        bool result = Validator.TryValidateObject(obj, validationContext, valiationResults, true);
        results = valiationResults;
        return result;
    }
}
