//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Data;

namespace CalculatorShell.Core.Mediator;

/// <summary>
/// Mediator implementation
/// </summary>
public sealed class Mediator : IMediator
{
    private readonly List<WeakReference> _clients;

    /// <summary>
    /// Creates a new instance of mediator
    /// </summary>
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

    /// <inheritdoc/>
    public void Notify<TMessage>(in TMessage payload)
        where TMessage : PayloadBase
    {
        bool cleanupNeeded = false;
        foreach (var client in _clients)
        {
            if (client.IsAlive)
            {
                if (client.Target is INotifyTarget<TMessage> notifyClient)
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

    /// <inheritdoc/>
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
                if (client.Target is IRequestProvider<TReturn, TMessage> providerClient)
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

    /// <inheritdoc/>
    public void Register<TClient>(in TClient client) where TClient : IMediatorComponent
    {
        _clients.Add(new WeakReference(client));
    }

    /// <inheritdoc/>
    public IEnumerable<TReturn> RequestAll<TReturn, TMessage>(TMessage message)
        where TReturn : class
        where TMessage : PayloadBase
    {
        bool cleanupNeeded = false;

        foreach (var client in _clients)
        {
            if (client.IsAlive)
            {
                if (client.Target is IRequestProvider<TReturn, TMessage> providerClient)
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