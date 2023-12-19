namespace CalculatorShell.Core.Messenger;

internal sealed class MessagingComponentReference
{
    private readonly WeakReference _clientReference;

    public MessagingComponentReference(IMessagingComponent client)
    {
        _clientReference = new WeakReference(client);
    }

    public bool IsAlive => _clientReference.IsAlive;

    public void Send<TMessage>(TMessage message, object lockObject) where TMessage : MessageBase
    {
        if (_clientReference.Target is IMessageClient<TMessage> client)
        {
            lock (lockObject)
            {
                client.ProcessMessage(message);
            }
        }
    }

    public TResult? Request<TResult, TMessage>(TMessage message, object lockObject) where TMessage : MessageBase
    {
        if (_clientReference.Target is IMessageProvider<TResult, TMessage> client)
        {
            lock (lockObject)
            {
                return client.ProvideMessage(message);
            }
        }
        return default;
    }
}
