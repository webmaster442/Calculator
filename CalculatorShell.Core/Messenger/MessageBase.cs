namespace CalculatorShell.Core.Messenger;

public abstract class MessageBase
{
    public DateTime DispatchTime { get; }
    public Guid SenderId { get; }

    public MessageBase(Guid sender)
    {
        DispatchTime = DateTime.Now;
        SenderId = sender;
    }

    public override string ToString()
    {
        return $"DispatchTime: {DispatchTime}{Environment.NewLine}Sender: {SenderId}";
    }
}
