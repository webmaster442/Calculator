//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core.Mediator;

public abstract class PayloadBase
{
    public DateTime DispatchTime { get; }

    protected PayloadBase()
    {
        DispatchTime = DateTime.Now;
    }
}
