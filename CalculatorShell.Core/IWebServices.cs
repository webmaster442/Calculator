//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Web services that are callable
/// </summary>
public interface IWebServices
{
    /// <summary>
    /// Current currency exchange rates
    /// </summary>
    /// <returns>Currency exchange rates by currency in HUF</returns>
    Task<Dictionary<string, decimal>> GetCurrencyRates();

    /// <summary>
    /// Flush cached values
    /// </summary>
    void FlushCache();
}
