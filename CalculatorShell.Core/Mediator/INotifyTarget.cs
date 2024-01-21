namespace CalculatorShell.Core.Mediator;

public interface INotifyTarget<in TMessage> : IMediatorComponent where TMessage : PayloadBase
{
    void OnNotify(TMessage message);
}
