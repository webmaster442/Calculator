//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Xml.Serialization;

namespace Calculator.Web.ServiceDTO.MnbCurrencyRates;

[Serializable]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public class MNBCurrentExchangeRates
{
    public required MNBCurrentExchangeRatesDay Day { get; set; }
}
