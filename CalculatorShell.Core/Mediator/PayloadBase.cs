//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core.Mediator;

/// <summary>
/// A base class for data request and notification messages
/// </summary>
public abstract class PayloadBase
{
    /// <summary>
    /// Message dispatch time
    /// </summary>
    public DateTime DispatchTime { get; }

    /// <summary>
    /// Creates a new instance of PayloadBase
    /// </summary>
    protected PayloadBase()
    {
        DispatchTime = DateTime.Now;
    }
}
