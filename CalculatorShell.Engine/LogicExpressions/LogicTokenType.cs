namespace CalculatorShell.Engine.LogicExpressions;

internal enum LogicTokenType : uint
{
    None = 0,
    Constant = 1,
    Variable = 2,
    Not = 4,
    Or = 8,
    And = 16,
    OpenParen = 0x20000000,
    CloseParen = 0x40000000,
    Eof = 0x80000000
}
