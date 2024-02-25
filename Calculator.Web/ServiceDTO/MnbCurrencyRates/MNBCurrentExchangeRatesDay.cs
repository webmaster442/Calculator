//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Xml.Serialization;

namespace Calculator.Web.ServiceDTO.MnbCurrencyRates;

[Serializable]
[XmlType(AnonymousType = true)]
public class MNBCurrentExchangeRatesDay
{
    [XmlElement("Rate")]
    public required MNBCurrentExchangeRatesDayRate[] Rate { get; set; }

    [XmlAttribute(DataType = "date", AttributeName = "date")]
    public System.DateTime Date { get; set; }
}
