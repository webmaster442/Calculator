namespace CalculatorShell.Core.Mediator;

public interface IRequestProvider<TReturn, TMessage> : IMediatorComponent where TMessage : PayloadBase
{
    TReturn OnRequest(TMessage message);
}
