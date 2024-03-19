//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Engine.Expressions;

[Flags]
internal enum TokenType : uint
{
    None = 0x00000000,
    Constant = 0x00000001,
    Variable = 0x00000002,
    Plus = 0x00000004,
    Minus = 0x00000008,
    Multiply = 0x00000010,
    Divide = 0x00000020,
    Mod = 0x00000040,
    Exponent = 0x00000080,
    Function = 0x00000100,
    ArgumentDivider = 0x00000200,
    GreaterThan = 0x00000400,
    GreaterThanEqual = 0x00000800,
    SmallerThan = 0x00001000,
    SmallerThanEqual = 0x00002000,
    Equal = 0x00004000,
    NotEqual = 0x00008000,
    TennaryCheck = 0x00010000,
    TennaryElse = 0x00020000,
    OpenParen = 0x20000000,
    CloseParen = 0x40000000,
    Eof = 0x80000000
}
