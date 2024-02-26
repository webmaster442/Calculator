//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace Calculator.Web.Server.Api;

[AttributeUsage(AttributeTargets.Method)]
public abstract class RouteAttribute : Attribute
{
    public string Url { get; set; }

    public RouteAttribute(string url)
    {
        Url = url;
    }
}
