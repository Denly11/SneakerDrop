using System.Globalization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Microsoft.Win32.SafeHandles;

namespace CurrencyApp.Models;

/// <summary>
/// One row of the CNB exchange rate table.
/// </summary>
public class CurrencyRate
{
    [XmlAttribute("zeme")]
    public string Country { get; set; } = string.Empty;
    [XmlAttribute("mena")]
    public string Name { get; set; } = string.Empty;
    [XmlAttribute("mnozstvi")]
    public int Amount { get; set; }
    [XmlAttribute("kod")]
    public string Code { get; set; } = string.Empty;

    [XmlIgnore]
    public decimal Rate { get; set; }

    [XmlAttribute("kurz")]
    [JsonIgnore]//nebudem tohleukládat dvakrát že jo(lokálně)
    public string RateString
    {
        get => Rate.ToString("N3", new CultureInfo("cs-CZ"));//N desetinná čísla jsou oddělena tečkou, zobraz 3 desetiná místa
        set => Rate = decimal.Parse(value, new CultureInfo("cs-CZ"));
    }

}
