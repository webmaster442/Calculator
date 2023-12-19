using System.Globalization;

namespace CalculatorShell.Engine;

public interface ILogicExpression
{
    bool Evaluate(IDictionary<string, bool> variables);
    string ToString(CultureInfo cultureInfo);
}
