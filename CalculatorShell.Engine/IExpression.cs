using System.Globalization;

namespace CalculatorShell.Engine;

public interface IExpression
{
    IExpression Simplify();
    Number Evaluate();
    string ToString(CultureInfo cultureInfo);
}
