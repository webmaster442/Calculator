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

    public ILogicExpression Parse(int variableCount, int[] minterms)
        => _logicParser.Parse(variableCount, minterms);
}
