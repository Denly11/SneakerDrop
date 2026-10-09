using System.Globalization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CurrencyApp.Part1.Models;

public class CurrencyRate
{
    // The CNB writes numbers with a decimal comma ("24,415").
    private static readonly CultureInfo CzechCulture = CultureInfo.GetCultureInfo("cs-CZ");

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

    /// <summary>
    /// <see cref="Rate"/> in the CNB format. XmlSerializer expects "24.415",
    /// so the Czech text is converted here.
    /// </summary>
    [XmlAttribute("kurz")]
    [JsonIgnore]
    public string RateText
    {
        get => Rate.ToString(CzechCulture);
        set => Rate = decimal.Parse(value, CzechCulture);
    }
}
