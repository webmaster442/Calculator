//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;
using System.Reflection;

using CalculatorShell.Core;

namespace Calculator.Web.Server.Api;

internal abstract class ApiRequestHandler : IRequestHandler
{
    private record ApiRoute
    {
        public string Method { get; }
        public string Path { get; }

        public ApiRoute(string method, string path)
        {
            Method = method;
            Path = path;
        }

        public static ApiRoute ToApiRoute(PostRouteAttribute postRouteAttribute)
            => new ApiRoute("POST", postRouteAttribute.Url);

        public static ApiRoute ToApiRoute(GetRouteAttribute getRouteAttribute)
            => new ApiRoute("GET", getRouteAttribute.Url);
    }


    private readonly Dictionary<ApiRoute, ApiMethod> _apiRoutes;

    public ILog Log { get; }

    protected ApiRequestHandler(ILog log)
    {
        Log = log;
        _apiRoutes = new Dictionary<ApiRoute, ApiMethod>();
        Load();
    }

    private void Load()
    {
        var candidates = this.GetType()
            .GetMethods()
            .Where(method => method.ReturnType == typeof(ApiResponse));

        foreach (var candidate in candidates)
        {
            try
            {
                if (candidate.GetCustomAttribute<PostRouteAttribute>() is PostRouteAttribute postRoute)
                {
                    _apiRoutes.Add(ApiRoute.ToApiRoute(postRoute), candidate.CreateDelegate<ApiMethod>());
                }
                else if (candidate.GetCustomAttribute<GetRouteAttribute>() is GetRouteAttribute getRoute)
                {
                    _apiRoutes.Add(ApiRoute.ToApiRoute(getRoute), candidate.CreateDelegate<ApiMethod>());
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }
    }

    public bool HandleRequest(HttpListenerContext context)
    {
        var handler = _apiRoutes
            .Where(route => context.IsMatch(route.Key.Method, route.Key.Path))
            .Select(route => route.Value)
            .FirstOrDefault();

        if (handler != null)
        {
            var result = handler.Invoke(context.Request);
            context.Transfer(result.Content, result.MimeType, result.StatusCode);
            return true;
        }

        return false;
    }
}
