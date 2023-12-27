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
