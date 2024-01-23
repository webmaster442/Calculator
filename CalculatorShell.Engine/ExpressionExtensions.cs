using CalculatorShell.Engine.LogicExpressions;
using CalculatorShell.Engine.Simplification;

using System.Linq.Expressions;

namespace CalculatorShell.Engine;

public static class ExpressionExtensions
{
    internal static IEnumerable<Expression> Flatten(this Expression expression)
    {
        Stack<Expression> expressions = new();
        expressions.Push(expression);

        while (expressions.Count > 0)
        {
            Expression? n = expressions.Pop();

            if (n != null)
            {
                yield return n;
            }

            if (n is BinaryExpression binary)
            {
                if (binary.Left != null)
                    expressions.Push(binary.Left);
                if (binary.Right != null)
                    expressions.Push(binary.Right);
            }
            else if (n is UnaryExpression unary
                && unary.Operand != null)
            {
                expressions.Push(unary.Operand);
            }
        }
    }

    private static IEnumerable<ILogicExpression> Flatten(this ILogicExpression expression)
    {
        Stack<ILogicExpression> expressions = new();
        expressions.Push(expression);

        while (expressions.Count > 0)
        {
            ILogicExpression? n = expressions.Pop();

            if (n != null)
            {
                yield return n;
            }

            if (n is BaseLogicOperationExpression @base)
            {
                if (@base.Left != null)
                    expressions.Push(@base.Left);
                if (@base.Right != null)
                    expressions.Push(@base.Right);
            }
            else if (n is NotLogicExpression not
                && not.Child != null)
            {
                expressions.Push(not.Child);
            }
        }
    }

    public static ILogicExpression Simplify(this ILogicExpression expression)
    {
        HashSet<int> minterms = new();
        var variables = expression
            .Flatten()
            .Reverse()
            .OfType<VariableLogicExpression>()
            .DistinctBy(x => x.Name)
            .ToDictionary(x => x.Name, _ => false);

        if (variables.Count == 0)
            return new ConstantLogicExpression(expression.Evaluate(new Dictionary<string, bool>()));

        for (int i = 0; i < (1 << variables.Count); i++)
        {
            SetVariable(variables, i);
            if (expression.Evaluate(variables))
            {
                minterms.Add(i);
            }
        }
        LogicExpressionParser parser = new();

        var simplified = QuineMcclusky.GetSimplified(minterms, variables.Keys.ToArray());

        return parser.Parse(simplified);
    }

    private static void SetVariable(Dictionary<string, bool> variables, int i)
    {
        string bin = Convert.ToString(i, 2).PadLeft(variables.Count, '0');
        foreach (var (First, Second) in variables.Keys.Zip(bin))
        {
            variables[First] = Second == '1' ? true : false;
        }
    }
}
