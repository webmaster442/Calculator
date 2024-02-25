//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Linq.Expressions;

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

    public Expression Compile()
        => Expression.Parameter(typeof(Number), _idendifier);

    public Number Evaluate()
        => _variables.Get(_idendifier);

    public IExpression Simplify()
        => new VariableExpression(_variables, _idendifier);

    public string ToString(CultureInfo cultureInfo)
        => _idendifier;
}
