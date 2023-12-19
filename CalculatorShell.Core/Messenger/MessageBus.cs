namespace CalculatorShell.Core.Messenger;

public sealed class MessageBus : IMessageBus
{
    private readonly Dictionary<Guid, MessagingComponentReference> _clients;
    private readonly object _lock;

    public MessageBus()
    {
        _lock = new object();
        _clients = new Dictionary<Guid, MessagingComponentReference>();
    }

    private void RemoveDead()
    {
        HashSet<Guid> deads = new();
        foreach (var client in _clients)
        {
            if (!client.Value.IsAlive)
                deads.Add(client.Key);
        }
        foreach (var dead in deads)
        {
            _clients.Remove(dead);
        }
    }

    public void Broadcast<TMessage>(TMessage message) where TMessage : MessageBase
    {
        RemoveDead();
        foreach (var client in _clients.Values)
        {
            client.Send(message, _lock);
        }
    }

    public void RegisterComponent(IMessagingComponent client)
    {
        RemoveDead();
        _clients.Add(client.ClientId, new MessagingComponentReference(client));
    }

    public void Send<TMessage>(Guid target, TMessage message) where TMessage : MessageBase
    {
        RemoveDead();
        if (_clients.TryGetValue(target, out MessagingComponentReference? messageTarget))
        {
            messageTarget.Send(message, _lock);
        }
    }

    public IEnumerable<TReturn> Request<TReturn, TMessage>(TMessage requestMessage) where TMessage : MessageBase
    {
        RemoveDead();
        foreach (var client in _clients)
        {
            var data = client.Value.Request<TReturn, TMessage>(requestMessage, _lock);
            if (data != null)
            {
                yield return data;
            }
        }
    }
}
