//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

public interface IWebServices
{
    Task<Dictionary<string, decimal>> GetCurrencyRates();
    void FlushCache();
}
