//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

using Calculator.Web.ServiceDTO.MnbCurrencyRates;

using CalculatorShell.Core;

namespace Calculator.Web;

public sealed class WebServices : IWebServices
{
    private readonly WebCache _cache;
    private readonly string _cacheFileName;

    public WebServices()
    {
        _cacheFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "calculator.webcache.json");
        _cache = TryLoadCache();
    }

    private WebCache TryLoadCache()
    {
        if (File.Exists(_cacheFileName))
        {
            using (var stream = File.OpenRead(_cacheFileName))
            {
                return JsonSerializer.Deserialize<WebCache>(stream) ?? new WebCache();
            }
        }
        return new WebCache();
    }

    private void SaveCache()
    {
        using (var stream = File.Create(_cacheFileName))
        {
            JsonSerializer.Serialize(stream, _cache);
        }
    }

    private static T? DeserializeXML<T>(string text) where T : class
    {
        using (var reader = XmlReader.Create(new StringReader(text)))
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            return xs.Deserialize(reader) as T;
        }
    }

    public async Task<Dictionary<string, decimal>> GetCurrencyRates()
    {
        if (DateTime.Now < _cache.CurrecyRatesValidTill)
            return _cache.CurrencyRates;

        await using (MnbCurrencyRates.MNBArfolyamServiceSoapClient client = new())
        {
            var response = await client.GetCurrentExchangeRatesAsync(new MnbCurrencyRates.GetCurrentExchangeRatesRequestBody());
            var rates = DeserializeXML<MNBCurrentExchangeRates>(response.GetCurrentExchangeRatesResponse1.GetCurrentExchangeRatesResult);
            if (rates != null)
            {
                UpdateCache(rates);
            }
        }

        return _cache.CurrencyRates;
    }

    private void UpdateCache(MNBCurrentExchangeRates rates)
    {
        var hun = new CultureInfo("HU-hu");

        _cache.CurrecyRatesValidTill = rates.Day.Date.AddHours(24);
        _cache.CurrencyRates.Clear();
        foreach (var rate in rates.Day.Rate)
        {
            decimal value = decimal.Parse(rate.Value, hun) / rate.Unit;
            _cache.CurrencyRates.Add(rate.Currency.ToUpper(), value);
        }
        _cache.CurrencyRates.Add("HUF", 1M);
        SaveCache();
    }

    public void FlushCache()
    {
        _cache.Reset();
        if (File.Exists(_cacheFileName))
        {
            File.Delete(_cacheFileName);
        }
    }
}
