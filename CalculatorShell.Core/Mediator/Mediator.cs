using System.Data;

namespace CalculatorShell.Core.Mediator;

public sealed class Mediator : IMediator
{
    private readonly List<WeakReference> _clients;

    public Mediator()
    {
        _clients = new List<WeakReference>();
    }

    private void Cleanup()
    {
        var alive = _clients.Where(x => x.IsAlive).ToList();
        _clients.Clear();
        _clients.AddRange(alive);
    }

    public void Notify<TMessage>(in TMessage payload)
        where TMessage : PayloadBase
    {
        bool cleanupNeeded = false;
        foreach (var client in _clients)
        {
            if (client.IsAlive)
            {
                if (client is INotifyTarget<TMessage> notifyClient)
                {
                    notifyClient.OnNotify(payload);
                }
            }
            else
            {
                cleanupNeeded = true;
            }
        }

        if (cleanupNeeded)
            Cleanup();
    }

    public TReturn? Request<TReturn, TMessage>(in TMessage message)
        where TMessage : PayloadBase
        where TReturn : class
    {
        bool cleanupNeeded = false;
        TReturn? result = default;
        foreach (var client in _clients)
        {
            if (client.IsAlive)
            {
                if (client is IRequestProvider<TReturn, TMessage> providerClient)
                {
                    result = providerClient.OnRequest(message);
                    break;
                }
            }
            else
            {
                cleanupNeeded = true;
            }
        }

        if (cleanupNeeded)
            Cleanup();

        return result;
    }

    public void Register<TClient>(TClient client) where TClient : IMediatorComponent
    {
        _clients.Add(new WeakReference(client));
    }

    public IEnumerable<TReturn> RequestAll<TReturn, TMessage>(TMessage message)
        where TReturn : class
        where TMessage : PayloadBase
    {
        bool cleanupNeeded = false;

        foreach (var client in _clients)
        {
            if (client.IsAlive)
            {
                if (client is IRequestProvider<TReturn, TMessage> providerClient)
                {
                    yield return providerClient.OnRequest(message);
                }
            }
            else
            {
                cleanupNeeded = true;
            }
        }

        if (cleanupNeeded)
            Cleanup();
    }
}