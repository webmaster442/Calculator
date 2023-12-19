using System.Globalization;

namespace CalculatorShell.Engine.Expressions;

internal class VariableExpression : IExpression
{
    private readonly IVariables _variables;
    private readonly string _idendifier;

    public VariableExpression(IVariables variables, string idendifier)
    {
        _variables = variables;
        _idendifier = idendifier;
    }

    public Number Evaluate()
        => _variables.Get(_idendifier);

    public IExpression Simplify()
        => new VariableExpression(_variables, _idendifier);

    public string ToString(CultureInfo cultureInfo)
        => _idendifier;
}
