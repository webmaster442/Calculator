//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Engine.Expressions;

internal interface IFunctionProvider
{
    IEnumerable<string> FunctionNames { get; }
    int ArgumentCount(string functionName);
    IExpression? CreateExpression(string name, Queue<IExpression> parameters);
}
