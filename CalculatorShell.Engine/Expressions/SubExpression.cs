using System.Globalization;

namespace CalculatorShell.Engine.Expressions;

internal sealed class SubExpression : BinaryExpression
{
    public SubExpression(IExpression left, IExpression right) : base(left, right)
    {
    }

    public override IExpression Simplify()
    {
        var newLeft = Left.Simplify();
        var newRight = Right.Simplify();

        var leftConst = newLeft as ConstantExpression;
        var rightConst = newRight as ConstantExpression;
        var rightNegate = newRight as NegateExpression;

        if (leftConst != null && rightConst != null)
        {
            // two constants;  just evaluate it;
            return new ConstantExpression(leftConst.Value - rightConst.Value);
        }
        if (leftConst?.Value.IsZero() == true)
        {
            // 0 - y;  return -y;
            if (rightNegate != null)
            {
                // y = -u (--u);  return u;
                return rightNegate.Child;
            }
            return new NegateExpression(newRight);
        }
        if (rightConst?.Value.IsZero() == true)
        {
            // x - 0;  return x;
            return newLeft;
        }
        if (rightNegate != null)
        {
            // x - -y;  return x + y;
            return new AddExpression(newLeft, rightNegate.Child);
        }
        // x - y;  no simplification
        return new SubExpression(newLeft, newRight);
    }

    public override string ToString(CultureInfo cultureInfo)
        => $"({Left.ToString(cultureInfo)} - {Right.ToString(cultureInfo)})";

    protected override Number Evaluate(Number number1, Number number2)
        => number1 - number2;
}
