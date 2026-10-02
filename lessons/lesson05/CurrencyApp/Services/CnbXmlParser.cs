using CurrencyApp.Models;

namespace CurrencyApp.Services;

/// <summary>
/// Reads the CNB XML file (denni_kurz.xml).
/// </summary>
public static class CnbXmlParser
{
    public static DailyRates Load(string filePath)
    {
        var xmlFile = XDocument.Load(filePath);//Xdocument třída pro prácí s xml soubory
        var culture = new CultureInfo("cs-CZ");//objekt s českými pravidly pro formátování dat a čísel
        var result = new DailyRates
        {
            Date = DateTime.ParseExact(xmlFile.Root!.Attribute("datum")!.Value, "dd.MM.yyyy", culture)
        };

        foreach (var row in xmlFile.Descendants("radek"))
        {
            result.Rates.Add(new CurrencyRate
            {
                Code = row.Attribute("kod")!.Value,
                Name = row.Attribute("mena")!.Value,
                Country = row.Attribute("zeme")!.Value,
                Amount = int.Parse(row.Attribute("mnozstvi")!.Value),
                Rate = decimal.Parse(row.Attribute("kurz")!.Value, culture)
            });
        }

        return result;


        throw new NotImplementedException();
    }
}
