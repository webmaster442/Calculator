namespace CalculatorShell.Core;

public interface IWebServices
{
    Task<Dictionary<string, decimal>> GetCurrencyRates();
    void FlushCache();
}
