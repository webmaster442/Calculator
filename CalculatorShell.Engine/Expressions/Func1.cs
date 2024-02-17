using System.Diagnostics;
using System.Reflection;

namespace CalculatorShell.Engine.Expressions;

public class Func1 : BaseFunction
{
    private readonly Func<Number, Number> _function;

    public Func1(MethodInfo methodInfo) : base(methodInfo)
    {
        _function = Delegate.CreateDelegate(typeof(Func<Number, Number>), MethodInfo) as Func<Number, Number>
            ?? throw new UnreachableException($"Delegate compile failed: {methodInfo.Name}");
    }

    public Number Evaluate(Number arg)
        => _function(arg);
}
