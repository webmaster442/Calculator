//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine.Expressions;

internal sealed class ConstantExpression : IExpression
{
    public ConstantExpression(Number value)
    {
        Value = value;
    }

    public Number Value { get; }

    public Number Evaluate() => Value;

    public IExpression Simplify()
        => new ConstantExpression(Value);

    public string ToString(CultureInfo cultureInfo)
        => Value.ToString(cultureInfo) ?? string.Empty;

    public Expression Compile() => Expression.Constant(Value);
}
