using System.Globalization;
using System.Linq.Expressions;

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
        _parser = new ExpressionParser(_functionProvider.SingleParameterFunctions,
                                       _functionProvider.DoubleParameterFunctions);
    }

    public IEnumerable<string> Functions
        => _functionProvider.SingleParameterFunctions.Keys.Concat(_functionProvider.DoubleParameterFunctions.Keys).Order();

    public IVariables Variables { get; }

    public CultureInfo Culture { get; set; }

    public AngleSystem AngleSystem
    {
        get => NumberMath.AngleSystem;
        set => NumberMath.AngleSystem = value;
    }

    public IExpression Parse(string expression)
        => _parser.Parse(expression, Culture, Variables, AngleSystem).Simplify();

    public Task<EngineResult> ExecuteAsync(string expression, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            try
            {
                IExpression expr = _parser.Parse(expression,
                                                 Culture,
                                                 Variables,
                                                 AngleSystem);

                return new EngineResult(expr.Evaluate());
            }
            catch (Exception ex)
            {
                return new EngineResult(ex);
            }
        }, cancellationToken);
    }

    public IEnumerable<(double x, double y)> Iterate(string expression, double from, double to, double steps)
    {
        var body = Parse(expression).Simplify().Compile();
        var parameters = body.Flatten().OfType<ParameterExpression>().ToArray();

        if (parameters.Length != 1)
            throw new EngineException("Expression must have only one variable parameter to iterate");

        Func<Number, Number> compiled = Expression.Lambda<Func<Number, Number>>(body, parameters).Compile();

        double current = from;
        double step = (to - from) / steps;
        while (current <= to)
        {
            yield return(current, compiled.Invoke(new Number(current)).ToDouble());
            current += step;
        }
    }
}
