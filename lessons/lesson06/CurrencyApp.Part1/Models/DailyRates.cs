using System.Globalization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CurrencyApp.Part1.Models;

[XmlRoot("kurzy")]
public class DailyRates
{
    private const string DateFormat = "dd.MM.yyyy";

    [XmlIgnore]
    public DateTime Date { get; set; }

    [XmlAttribute("datum")]
    [JsonIgnore]
    public string DateText
    {
        get => Date.ToString(DateFormat, CultureInfo.InvariantCulture);
        set => Date = DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
    }

    [XmlArray("tabulka")]
    [XmlArrayItem("radek")]
    public List<CurrencyRate> Rates { get; set; } = [];
}
