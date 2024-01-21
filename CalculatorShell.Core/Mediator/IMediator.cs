namespace CalculatorShell.Core.Mediator;

public interface IMediator
{
    public void Notify<TMessage>(in TMessage payload) where TMessage : PayloadBase;
    public TReturn? Request<TReturn, TMessage>(in TMessage message) where TMessage : PayloadBase where TReturn : class;
    public IEnumerable<TReturn> RequestAll<TReturn, TMessage>(TMessage message) where TMessage : PayloadBase where TReturn : class;
    public void Register<TClient>(TClient client) where TClient : IMediatorComponent;
}
