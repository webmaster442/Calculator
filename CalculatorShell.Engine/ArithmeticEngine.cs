//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

using CalculatorShell.Engine.Algortihms;
using CalculatorShell.Engine.Expressions;

namespace CalculatorShell.Engine;

public sealed class ArithmeticEngine : IArithmeticEngine
{
    private readonly ExpressionParser _parser;
    private readonly FunctionProvider _functionProvider;

    public ArithmeticEngine(IVariables variables, CultureInfo culture)
    {
        Variables = variables;
        Culture = culture;
        AngleSystem = AngleSystem.Deg;
        _functionProvider = new();
        _parser = new ExpressionParser(_functionProvider);
    }

    public IEnumerable<string> Functions
        => _functionProvider.FunctionNames;

    public IVariables Variables { get; }

    public CultureInfo Culture { get; set; }

    public AngleSystem AngleSystem
    {
        get => NumberMath.AngleSystem;
        set => NumberMath.AngleSystem = value;
    }

    public async Task<IExpression> ParseAsync(string expression, CancellationToken cancellationToken = default)
    {
        var exp = await _parser.Parse(expression, Culture, Variables, cancellationToken);
        return exp.Simplify();
    }

    public async Task<EngineResult> ExecuteAsync(string expression, CancellationToken cancellationToken = default)
    {
        try
        {
            IExpression expr = await _parser.Parse(expression,
                                                   Culture,
                                                   Variables,
                                                   cancellationToken);

            return new EngineResult(expr.Evaluate());
        }
        catch (Exception ex)
        {
            return new EngineResult(ex);
        }
    }

    public async IAsyncEnumerable<(double x, double y)> Iterate(string expression,
                                                                double from,
                                                                double to,
                                                                double steps,
                                                                [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var expr = await ParseAsync(expression, cancellationToken);
        var body = expr.Simplify().Compile();
        var parameters = ExpressionFlattener.Flatten(body).OfType<ParameterExpression>().ToArray();

        if (parameters.Length != 1)
            throw new EngineException("Expression must have only one variable parameter to iterate");

        Func<Number, Number> compiled = Expression.Lambda<Func<Number, Number>>(body, parameters).Compile();

        double current = from;
        double step = (to - from) / steps;
        while (current <= to)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            yield return (current, compiled.Invoke(new Number(current)).ToDouble());
            current += step;
        }
    }
}
