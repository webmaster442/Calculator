namespace CalculatorShell.Core.Messenger;

public interface IMessageClient<in TMessage> : IMessagingComponent where TMessage : MessageBase
{
    void ProcessMessage(TMessage input);
}
