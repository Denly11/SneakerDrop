//Part 2 – uncomment when you get to it.
 using System.IO;
 using System.Net.Http;
 using CurrencyApp.Models;

 namespace CurrencyApp.Services;

//Downloads the daily exchange rates from the CNB web.

public class CnbClient
{
    private const string BaseUrl =
        "https://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.xml";

    private static readonly HttpClient HttpClient = new();// zkrácený zápis: ... = new HttpClient();

    public async Task<DailyRates> GetRatesAsync(DateTime date)
    {
        string url = $"{BaseUrl}?date={date:dd.MM.yyyy}";//date aby se vracely data i z jiných datumů DDDD:
        using Stream stream = await HttpClient.GetStreamAsync(url);//stream dat z url
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(DailyRates));
        //převaděč z xml do objektu; objekt co něco umí dělat; = ; typeof vytáhne šablonu daný věci a to je to do čeho se to přetváří z xml
        return (DailyRates)serializer.Deserialize(stream)!;
        //metoda sice vrací objekt Dailyrates ale compilátor je "retard"
        //(DailyRates) definuje datový typ("název krabice - co v ní je")
        //deserializer dělá něco z xml
        //zdroj toho xml filu c ose bude deserializovat je...
    }
}