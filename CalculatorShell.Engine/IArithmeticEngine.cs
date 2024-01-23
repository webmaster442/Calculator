using System.Globalization;

namespace CalculatorShell.Engine;

public interface IArithmeticEngine
{
    IVariables Variables { get; }
    CultureInfo Culture { get; set; }
    AngleSystem AngleSystem { get; set; }
    Task<EngineResult> ExecuteAsync(string expression, CancellationToken cancellationToken);
    IExpression Parse(string expression);
    IEnumerable<(double x, double y)> Iterate(string expression, double from, double to, double steps);

}
