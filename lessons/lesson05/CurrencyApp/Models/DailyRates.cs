using System.Globalization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CurrencyApp.Models;

/// <summary>
/// Exchange rate list published by the CNB for a single day.
/// </summary>
[XmlRoot("kurzy")]
//objekt daily rates obsahuje informaci že v xml podobì tento soubor má root tag: kurzy
//píše se to nad tøídu, pro jmného root tagu syntaxe: [XmlRoot("nazev")]
public class DailyRates
{
    [XmlIgnore]//ènb soubor nic jako Id nemá, serilizer by se jinak snažil najít tag: "Id"
    public Guid Id { get; set; } = Guid.NewGuid();
    //zadání vyžaduje "jedineèný identifikátor".

    [XmlIgnore]
    public DateTime Date { get; set; }

    [XmlIgnore]
    public DateTime DownloadedAt { get; set; } = DateTime.Now;
    //zadání vyžaduje vlastnost pro èas stažení

    [XmlAttribute("datum")]
    [JsonIgnore]//tohle nechci aby se lokálnì ukládalo v jsonu
    public string DateString
        //musí to být takhle bokem protže xml serilizer funguje v formátu YYYY-MM-DD
        //pro serilizer to je jenom další string
    {
        get => Date.ToString("dd.MM.yyyy");//serilizer neumí pøevádìt d oèeského formátu èasu
        set => Date = DateTime.ParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
    //set - když se to stáhne a z stringu se datum pøevede do DT date
    //pokud get ètu tak: z nìj udìlá string v danném formátu
    }

    [XmlArray("tabulka")]//obalovací tag pro seznam
    [XmlArrayItem("radek")]//definuje položku v seznamu
    //array v Rates se v xml jmenuje tabulka a každá položka rates je radek
    public List<CurrencyRate> Rates { get; set; } = [];
}
