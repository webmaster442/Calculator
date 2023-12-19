namespace CalculatorShell.Engine;

public interface ILogicEngine
{
    ILogicExpression Parse(int variableCount, int[] minterms);
    ILogicExpression Parse(string expression);
}