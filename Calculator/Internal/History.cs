using Calculator.Messages;

using CalculatorShell.Core.Mediator;

namespace Calculator.Internal;
internal class History
    : IRequestProvider<IEnumerable<string>, HistoryRequestMessage>,
      INotifyTarget<AddHistory>
{
    private readonly IMediator _mediator;
    private readonly List<(string cmdline, bool success)> _history;

    public History(IMediator mediator)
    {
        _mediator = mediator;
        _history = [];
    }

    public void RegisterToMediator()
    {
        _mediator.Register(this);
    }

    void INotifyTarget<AddHistory>.OnNotify(AddHistory message)
    {
        _history.Add((message.CommandLine, message.Success));
    }

    IEnumerable<string> IRequestProvider<IEnumerable<string>, HistoryRequestMessage>.OnRequest(HistoryRequestMessage message)
    {
        if (message.Kind == HistoryRequestMessage.HistoryKind.All)
            return _history.Select(x => x.cmdline);

        return _history
            .Where(x => x.success)
            .Select(x => x.cmdline);
    }
}
