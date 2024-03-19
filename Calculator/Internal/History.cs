//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Messages;

using CalculatorShell.Core.Mediator;

namespace Calculator.Internal;
internal class History
    : IRequestProvider<IEnumerable<string>, HistoryRequestMessage>,
      INotifyTarget<AddHistory>,
      INotifyTarget<DeleteHistory>
{
    private readonly IMediator _mediator;
    private readonly List<string> _history;

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
        _history.Add(message.CommandLine);
    }

    void INotifyTarget<DeleteHistory>.OnNotify(DeleteHistory message)
    {
        _history.Clear();
    }

    IEnumerable<string> IRequestProvider<IEnumerable<string>, HistoryRequestMessage>.OnRequest(HistoryRequestMessage message)
    {
        return _history;
    }
}
