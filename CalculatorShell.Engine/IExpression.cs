using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine;

public interface IExpression
{
    IExpression Simplify();
    Number Evaluate();
    string ToString(CultureInfo cultureInfo);
    Expression Compile();
}
