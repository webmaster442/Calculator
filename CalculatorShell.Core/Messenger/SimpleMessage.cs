namespace CalculatorShell.Core.Messenger;

public class SimpleMessage<T> : MessageBase
{
    public T Payload { get; }

    public SimpleMessage(Guid sender, T payload) : base(sender)
    {
        Payload = payload;
    }

    public override string ToString()
    {
        return $"DispatchTime: {DispatchTime}{Environment.NewLine}Sender: {SenderId}{Environment.NewLine}Payload: {Payload}";
    }
}
