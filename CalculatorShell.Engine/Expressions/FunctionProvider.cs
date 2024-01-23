using System.Data;
using System.Reflection;

using CalculatorShell.Engine.Algortihms;

namespace CalculatorShell.Engine.Expressions;

internal class FunctionProvider
{
    private readonly Dictionary<string, SingleParameterFunction> _singleParam;
    private readonly Dictionary<string, DoubleParameterFunction> _doublePram;

    public IReadOnlyDictionary<string, SingleParameterFunction> SingleParameterFunctions
        => _singleParam;

    public IReadOnlyDictionary<string, DoubleParameterFunction> DoubleParameterFunctions
        => _doublePram;

    public FunctionProvider()
    {
        _singleParam = new(StringComparer.OrdinalIgnoreCase);
        _doublePram = new(StringComparer.OrdinalIgnoreCase);

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
            if (parameters.Length == 1)
            {
                _singleParam.Add(candidate.Name, new SingleParameterFunction(candidate));
            }
            if (parameters.Length == 2)
            {
                _doublePram.Add(candidate.Name, new DoubleParameterFunction(candidate));
            }
        }
    }
}
