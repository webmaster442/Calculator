//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Engine.Simplification;

internal sealed class ImplicantRelationship
{
    public Implicant A { get; set; }
    public Implicant B { get; set; }

    public ImplicantRelationship(Implicant first, Implicant second)
    {
        A = first;
        B = second;
    }
}
