using System.Xml.Serialization;

namespace Calculator.Web.ServiceDTO.MnbCurrencyRates;

[Serializable]
[XmlType(AnonymousType = true)]
public class MNBCurrentExchangeRatesDayRate
{
    [XmlAttribute("unit")]
    public int Unit { get; set; }

    [XmlAttribute("curr")]
    public required string Currency { get; set; }

    [XmlText()]
    public required string Value { get; set; }
}