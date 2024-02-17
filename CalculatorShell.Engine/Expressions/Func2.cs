using System.Diagnostics;
using System.Reflection;

namespace CalculatorShell.Engine.Expressions;

public class Func2 : BaseFunction
{

    private readonly Func<Number, Number, Number> _function;

    public Func2(MethodInfo methodInfo) : base(methodInfo)
    {
        _function = Delegate.CreateDelegate(typeof(Func<Number, Number, Number>), MethodInfo) as Func<Number, Number, Number>
            ?? throw new UnreachableException("Delegate compile failed");
    }

    public Number Evaluate(Number arg1, Number arg2)
        => _function.Invoke(arg1, arg2);
}
