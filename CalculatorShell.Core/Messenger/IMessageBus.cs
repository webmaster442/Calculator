namespace CalculatorShell.Core.Messenger;

public interface IMessageBus
{
    void Send<TMessage>(Guid target, TMessage message) where TMessage : MessageBase;
    void Broadcast<TMessage>(TMessage message) where TMessage : MessageBase;
    void RegisterComponent(IMessagingComponent component);
    IEnumerable<TReturn> Request<TReturn, TMessage>(TMessage requestMessage) where TMessage : MessageBase;
}
