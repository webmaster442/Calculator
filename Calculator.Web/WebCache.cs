//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace Calculator.Web;
public class WebCache
{
    public Dictionary<string, decimal> CurrencyRates { get; init; }
    public DateTime CurrecyRatesValidTill { get; set; }

    public WebCache()
    {
        CurrecyRatesValidTill = DateTime.MinValue;
        CurrencyRates = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
    }

    internal void Reset()
    {
        CurrecyRatesValidTill = DateTime.MinValue;
        CurrencyRates.Clear();
    }
}
