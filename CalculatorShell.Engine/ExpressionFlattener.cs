//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace CalculatorShell.Engine;
internal static class ExpressionFlattener
{
    public static IEnumerable<Expression> Flatten(Expression expr)
    {
        return Visitor.Flatten(expr);
    }

    private sealed class Visitor : ExpressionVisitor
    {
        private readonly Action<Expression?> nodeAction;

        private Visitor(Action<Expression?> nodeAction)
        {
            this.nodeAction = nodeAction;
        }

        [return: NotNullIfNotNull("node")]
        public override Expression? Visit(Expression? node)
        {
            nodeAction(node);
            return base.Visit(node);
        }

        public static IEnumerable<Expression> Flatten(Expression expr)
        {
            var ret = new List<Expression>();
            var visitor = new Visitor(t =>
            {
                if (t != null)
                    ret.Add(t);
            });
            visitor.Visit(expr);
            return ret;
        }
    }
}

