namespace CalculatorShell.Core.Mediator;

public interface IRequestProvider<TReturn, TMessage> : IMediatorComponent where TMessage : PayloadBase where TReturn : class
{
    TReturn OnRequest(TMessage message);
}
