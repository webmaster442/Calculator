using System.Globalization;
using System.Linq.Expressions;

namespace CalculatorShell.Engine.Expressions;

internal abstract class BinaryExpression : IExpression
{
    protected IExpression Left { get; }

    protected IExpression Right { get; }

    protected BinaryExpression(IExpression left, IExpression right)
    {
        Left = left;
        Right = right;
    }

    public Number Evaluate()
        => Evaluate(Left.Evaluate(), Right.Evaluate());

    public abstract IExpression Simplify();

    protected abstract Number Evaluate(Number number1, Number number2);

    public abstract string ToString(CultureInfo cultureInfo);

    public override string ToString()
        => ToString(CultureInfo.InvariantCulture);

    public abstract Expression Compile();
}
