﻿namespace CalculatorShell.Engine.Expressions;

[Flags]
internal enum TokenType : uint
{
    None = 0,
    Constant = 1,
    Variable = 2,
    Plus = 4,
    Minus = 8,
    Multiply = 16,
    Divide = 32,
    Mod = 64,
    Exponent = 128,
    Function = 256,
    ArgumentDivider = 512,

    OpenParen = 0x20000000,
    CloseParen = 0x40000000,
    Eof = 0x80000000
}
