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
        var result = new DailyRates//třída jejíž objekt obsahuje datum a seznam měnových kurzů
        {
            Date = DateTime.ParseExact(xmlFile.Root!.Attribute("datum")!.Value, "dd.MM.yyyy", culture)
            //ParseExact převede text na datetime Exact tam potom dělá to že to je v přesném formátu
            //.Root vrátá jenom tu první část souboru, kde u cnb tam je mimo jiné TO DATUM, nějakej kořenovej element IDK, <elemnt datum,...> další elementy...</element>
            //kompilátor si myslí že někdy může být root elemnt nula já si myslím že ne a "!" ho umlčím
            //.Attribute("datum")! - vybere atribut datum z root elementu
            //.Value y toho atributu vezme hodnotu
            //"dd.MM.yyyy" - formát datumu pro Exact v ParseExact
            //culture určuje kulturní pravidla 
        };

        foreach (var row in xmlFile.Descendants("radek"))
        {//Descendants je metoda z LINQ která najde všechny elementy co nazvem odpovídají argementu, elementy s kurzy se tady nazývaj "radek"

            result.Rates.Add(new CurrencyRate//rates je vlastnost třídy DailyRates
            {
                Code = row.Attribute("kod")!.Value,//sloupky z UI...
                Name = row.Attribute("mena")!.Value,
                Country = row.Attribute("zeme")!.Value,
                Amount = int.Parse(row.Attribute("mnozstvi")!.Value),
                Rate = decimal.Parse(row.Attribute("kurz")!.Value, culture)
            });
        }

        return result;//objekt ve kterám je datum a sloupky s kodem menou zemí mnozstvim a kurzem


        //throw new NotImplementedException(); už je
    }
}
