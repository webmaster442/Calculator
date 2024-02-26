//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace Calculator.Web.Server.Api;

[AttributeUsage(AttributeTargets.Method)]
public class PostRouteAttribute : RouteAttribute
{
    public PostRouteAttribute(string url) : base(url)
    {
    }
}
