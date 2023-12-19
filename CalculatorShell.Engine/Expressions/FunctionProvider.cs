using System.Data;
using System.Reflection;

using CalculatorShell.Engine.Algortihms;

namespace CalculatorShell.Engine.Expressions;

public delegate Number SingleParamFunction(Number arg);
public delegate Number DoubleParamFunction(Number arg1, Number arg2);

internal class FunctionProvider
{
    private readonly Dictionary<string, SingleParamFunction> _singleParam;
    private readonly Dictionary<string, DoubleParamFunction> _doublePram;

    public IReadOnlyDictionary<string, SingleParamFunction> SingleParameterFunctions
        => _singleParam;

    public IReadOnlyDictionary<string, DoubleParamFunction> DoubleParameterFunctions
        => _doublePram;

    public FunctionProvider()
    {
        _singleParam = new();
        _doublePram = new();

        Fill(typeof(NumberMath));
    }

    private void Fill(Type type)
    {
        var candidates = type
            .GetMethods()
            .Where(m => m.IsStatic && m.GetCustomAttribute<EngineFunctionAttribute>() != null);

        foreach (var candidate in candidates)
        {
            var parameters = candidate.GetParameters();
            if (parameters.Any(p => p.ParameterType != typeof(Number)))
            {
                continue;
            }
            if (parameters.Length == 1
                && Delegate.CreateDelegate(typeof(SingleParamFunction), candidate) is SingleParamFunction f1)
            {
                _singleParam.Add(candidate.Name.ToLower(), f1);
            }
            if (parameters.Length == 2
                && Delegate.CreateDelegate(typeof(DoubleParamFunction), candidate) is DoubleParamFunction f2)
            {
                _doublePram.Add(candidate.Name.ToLower(), f2);
            }
        }
    }
}
