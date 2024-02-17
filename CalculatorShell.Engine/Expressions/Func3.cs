using System.Diagnostics;
using System.Reflection;

namespace CalculatorShell.Engine.Expressions;

public class Func3 : BaseFunction
{
    private readonly Func<Number, Number, Number, Number> _function;

    public Func3(MethodInfo methodInfo) : base(methodInfo)
    {
        _function = Delegate.CreateDelegate(typeof(Func<Number, Number, Number, Number>), MethodInfo) as Func<Number, Number, Number, Number>
            ?? throw new UnreachableException($"Delegate compile failed: {methodInfo.Name}");
    }

    public Number Evaluate(Number arg1, Number arg2, Number arg3)
        => _function(arg1, arg2, arg3);
}
