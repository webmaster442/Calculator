using System.Globalization;

namespace CalculatorShell.Engine.Expressions;

internal abstract class UnaryExpression : IExpression
{
    public IExpression Child { get; }

    protected UnaryExpression(IExpression child)
    {
        Child = child;
    }

    public Number Evaluate()
        => Evaluate(Child.Evaluate());

    protected abstract Number Evaluate(Number number);

    public abstract IExpression Simplify();

    public abstract string ToString(CultureInfo cultureInfo);

    public override string ToString()
        => ToString(CultureInfo.InvariantCulture);

    public abstract System.Linq.Expressions.Expression Compile();
}
