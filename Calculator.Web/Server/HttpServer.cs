//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;
using System.Net.NetworkInformation;

using CalculatorShell.Core;

namespace Calculator.Web.Server;

public sealed class HttpServer : IDisposable
{
    private readonly HttpListener _listener;
    private readonly Thread _listenerThread;
    private readonly Thread[] _workers;
    private readonly ManualResetEvent _stop, _ready;
    private readonly Queue<HttpListenerContext> _queue;
    private readonly ILog _log;
    private IReadOnlyCollection<IRequestHandler> _handlers;
    private readonly DefaultRequestHandler _defaultHandler;

    public HttpServer(ILog log,
                      int maxThreads = 2)
    {
        _defaultHandler = new DefaultRequestHandler();

        _workers = new Thread[maxThreads];
        _queue = new Queue<HttpListenerContext>();
        _stop = new ManualResetEvent(false);
        _ready = new ManualResetEvent(false);
        _listener = new HttpListener();
        _listenerThread = new Thread(HandleRequests);
        _log = log;
        _handlers = Array.Empty<IRequestHandler>();
    }

    public int Start(IReadOnlyCollection<IRequestHandler> handlers, int startPort = 11111, int endPort = 11211)
    {
        int port = FindAvailablePort(startPort, endPort);

        if (port == -1)
        {
            _log.Error($"Failed to start server, because no avaliable port can be found in range {startPort} {endPort}");
            return -1;
        }

        _handlers = handlers;
        _listener.Prefixes.Add($"http://127.0.0.1:{port}/");
        _listener.Prefixes.Add($"http://localhost:{port}/");
        _listener.Start();
        _listenerThread.Start();

        for (int i = 0; i < _workers.Length; i++)
        {
            _workers[i] = new Thread(Worker);
            _workers[i].Start();
        }
        _log.Info($"Server started on http://localhost:{port}/");

        return port;
    }

    private static int FindAvailablePort(int startPort, int endPort)
    {
        IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        var portsInUse = ipGlobalProperties.GetActiveTcpConnections().Select(x => x.LocalEndPoint.Port).ToHashSet();
        for (int port=startPort; port<=endPort; port++)
        {
            if (!portsInUse.Contains(port))
            {
                return port;
            }
        }
        return -1;
    }

    public void Dispose() => Stop();

    public void Stop()
    {
        _stop.Set();
        _listenerThread.Join();
        foreach (Thread worker in _workers)
        {
            worker.Join();
        }
        (_listener as IDisposable).Dispose();
        _stop.Dispose();
        _ready.Dispose();
    }

    private void HandleRequests()
    {
        while (_listener.IsListening)
        {
            var context = _listener.BeginGetContext(ContextReady, null);

            if (WaitHandle.WaitAny([_stop, context.AsyncWaitHandle]) == 0)
                return;
        }
    }

    private void ContextReady(IAsyncResult ar)
    {
        try
        {
            lock (_queue)
            {
                _queue.Enqueue(_listener.EndGetContext(ar));
                _ready.Set();
            }
        }
        catch { return; }
    }

    private void Worker()
    {
        WaitHandle[] wait = new[] { _ready, _stop };
        while (WaitHandle.WaitAny(wait) == 0)
        {
            HttpListenerContext context;
            lock (_queue)
            {
                if (_queue.Count > 0)
                {
                    context = _queue.Dequeue();
                }
                else
                {
                    _ready.Reset();
                    continue;
                }
            }

            try
            {
                bool handled = false;
                foreach (var handler in _handlers)
                {
                    if (handler.HandleRequest(context))
                    {
                        _log.Info($"Handling: {context.ToLogMessage()} with {handler.GetType().Name}");
                        handled = true;
                        break;
                    }
                }

                if (!handled)
                {
                    _log.Warning($"No handler found for request: {context.ToLogMessage()}");
                    _defaultHandler.HandleRequest(context);
                }

            }
            catch (Exception e)
            {
                _log.Error($"{e.Message}");
            }
        }
    }
}
