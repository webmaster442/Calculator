//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Engine.LogicExpressions;

namespace CalculatorShell.Engine;
public sealed class LogicEngine : ILogicEngine
{
    private readonly LogicExpressionParser _logicParser;

    public LogicEngine()
    {
        _logicParser = new LogicExpressionParser();
    }

    public ILogicExpression Parse(string expression)
        => _logicParser.Parse(expression);

    public ILogicExpression Parse(int variableCount, IReadOnlyList<int> minterms)
        => _logicParser.Parse(variableCount, minterms);
}
