//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Engine;

public interface ILogicEngine
{
    ILogicExpression Parse(int variableCount, IReadOnlyList<int> minterms);
    ILogicExpression Parse(string expression);
}