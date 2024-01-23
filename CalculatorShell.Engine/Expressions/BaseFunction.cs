using System.Reflection;

namespace CalculatorShell.Engine.Expressions;

public abstract class BaseFunction
{
    protected BaseFunction(MethodInfo methodInfo)
    {
        MethodInfo = methodInfo;
    }

    public MethodInfo MethodInfo { get; }
}
