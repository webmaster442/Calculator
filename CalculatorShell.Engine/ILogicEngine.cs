namespace CalculatorShell.Engine;

public interface ILogicEngine
{
    ILogicExpression Parse(int variableCount, IReadOnlyList<int> minterms);
    ILogicExpression Parse(string expression);
}