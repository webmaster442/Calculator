using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;
internal class HistoryRequestMessage : PayloadBase
{
    public enum HistoryKind
    {
        All,
        Successfull,
    }

    public HistoryKind Kind { get; }

    public HistoryRequestMessage(HistoryKind kind)
    {
        Kind = kind;
    }
}
