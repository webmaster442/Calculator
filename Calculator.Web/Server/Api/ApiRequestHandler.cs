//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;
using System.Reflection;

using CalculatorShell.Core;

namespace Calculator.Web.Server.Api;

public abstract class ApiRequestHandler<THandlerType> : IRequestHandler
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
            => new("POST", postRouteAttribute.Url);

        public static ApiRoute ToApiRoute(GetRouteAttribute getRouteAttribute)
            => new("GET", getRouteAttribute.Url);
    }


    private readonly Dictionary<ApiRoute, ApiMethod> _apiRoutes;
    private readonly Dictionary<ApiRoute, LowLevelApiMethod> _lowLevels;

    public IStructuredLog Log { get; }

    protected ApiRequestHandler(IStructuredLog log)
    {
        Log = log;
        _apiRoutes = Load<ApiMethod>(typeof(ApiResponse));
        _lowLevels = Load<LowLevelApiMethod>(typeof(void));

    }

    private Dictionary<ApiRoute, TDelegate> Load<TDelegate>(Type delegateReturnType)
        where TDelegate : Delegate
    {
        Dictionary<ApiRoute, TDelegate> results = new();

        var candidates = typeof(THandlerType)
            .GetMethods()
            .Where(method => method.ReturnType == delegateReturnType);

        foreach (var candidate in candidates)
        {
            try
            {
                if (candidate.GetCustomAttribute<PostRouteAttribute>() is PostRouteAttribute postRoute)
                {
                    results.Add(ApiRoute.ToApiRoute(postRoute), candidate.CreateDelegate<TDelegate>(this));
                }
                else if (candidate.GetCustomAttribute<GetRouteAttribute>() is GetRouteAttribute getRoute)
                {
                    results.Add(ApiRoute.ToApiRoute(getRoute), candidate.CreateDelegate<TDelegate>(this));
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        return results;
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

        var lowLevelHandler = _lowLevels
            .Where(route => context.IsMatch(route.Key.Method, route.Key.Path))
            .Select(route => route.Value)
            .FirstOrDefault();

        if (lowLevelHandler != null)
        {
            lowLevelHandler.Invoke(context);
            return true;
        }

        return false;
    }
}
