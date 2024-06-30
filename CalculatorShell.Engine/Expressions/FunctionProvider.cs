//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Data;
using System.Reflection;

using CalculatorShell.Engine.Algortihms;

namespace CalculatorShell.Engine.Expressions;

internal sealed class FunctionProvider : IFunctionProvider
{
    private record class FunctionEntry(int ParamCount, BaseFunction Function);

    private readonly Dictionary<string, FunctionEntry> _functions;
    public FunctionProvider()
    {
        _functions = new Dictionary<string, FunctionEntry>(StringComparer.InvariantCultureIgnoreCase);
        Fill(typeof(NumberMath));
    }

    private void Fill(Type type)
    {
        var candidates = type
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.IsStatic && m.GetCustomAttribute<EngineFunctionAttribute>() != null);

        foreach (var candidate in candidates)
        {
            var parameters = candidate.GetParameters();
            if (parameters.Any(p => p.ParameterType != typeof(Number)))
            {
                continue;
            }
            if (parameters.Length == 1)
                _functions.Add(candidate.Name, new FunctionEntry(parameters.Length, new Func1(candidate)));
            else if (parameters.Length == 2)
                _functions.Add(candidate.Name, new FunctionEntry(parameters.Length, new Func2(candidate)));
            else if (parameters.Length == 3)
                _functions.Add(candidate.Name, new FunctionEntry(parameters.Length, new Func3(candidate)));
        }
    }

    public IEnumerable<string> FunctionNames
        => _functions.Keys.Order();


    public int ArgumentCount(string functionName)
    {
        if (_functions.TryGetValue(functionName, out FunctionEntry? value))
            return value.ParamCount;

        return -1;
    }

    public IExpression? CreateExpression(string name, Queue<IExpression> parameters)
    {
        int count = ArgumentCount(name);
        return count switch
        {
            1 => new Func1Expression(parameters.Dequeue(), (Func1)_functions[name].Function, name),
            2 => new Func2Expression(parameters.Dequeue(), parameters.Dequeue(), (Func2)_functions[name].Function, name),
            3 => new Func3Expression(parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue(), (Func3)_functions[name].Function, name),
            _ => null,
        };
    }
}
