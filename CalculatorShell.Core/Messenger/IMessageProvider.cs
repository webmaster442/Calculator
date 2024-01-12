namespace CalculatorShell.Core.Messenger;

public interface IMessageProvider<out TReturn, in TMessage> : IMessagingComponent where TMessage : MessageBase
{
    TReturn ProvideMessage(TMessage request);
}
