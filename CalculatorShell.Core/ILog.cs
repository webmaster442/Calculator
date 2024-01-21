namespace CalculatorShell.Core;

public interface ILog
{
    void Exception(Exception ex);
    void Info(FormattableString text);
    void Warning(FormattableString text);
    void Error(FormattableString text);
}
