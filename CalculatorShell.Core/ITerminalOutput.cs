namespace CalculatorShell.Core;

public interface ITerminalOutput
{
    void BlankLine();
    void Clear();
    void Error(Exception ex);
    void Error(string message);
    void List(string header, IEnumerable<string> data);
    void Result(string message);
    void MarkupLine(string markup);
}
