//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Engine;

public sealed record class JsonNumber
{
    public required string Type { get; init; }
    public required string Value { get; init; }
}
