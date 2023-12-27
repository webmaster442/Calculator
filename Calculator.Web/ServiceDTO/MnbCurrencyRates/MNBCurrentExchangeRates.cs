using System.Xml.Serialization;

namespace Calculator.Web.ServiceDTO.MnbCurrencyRates;

[Serializable]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public class MNBCurrentExchangeRates
{
    public required MNBCurrentExchangeRatesDay Day { get; set; }
}
