//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

namespace CalculatorShell.Engine;

public interface IArithmeticEngine
{
    IVariables Variables { get; }
    CultureInfo Culture { get; set; }
    AngleSystem AngleSystem { get; set; }
    IEnumerable<string> Functions { get; }
    Task<EngineResult> ExecuteAsync(string expression, CancellationToken cancellationToken = default);
    IAsyncEnumerable<(double x, double y)> Iterate(string expression, double from, double to, double steps, CancellationToken cancellationToken = default);
    Task<IExpression> ParseAsync(string expression, CancellationToken cancellationToken = default);

}
