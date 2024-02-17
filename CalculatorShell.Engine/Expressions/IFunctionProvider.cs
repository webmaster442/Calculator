namespace CalculatorShell.Engine.Expressions;

internal interface IFunctionProvider
{
    IEnumerable<string> FunctionNames { get; }
    int ArgumentCount(string functionName);
    IExpression? CreateExpression(string name, Queue<IExpression> parameters);
}
