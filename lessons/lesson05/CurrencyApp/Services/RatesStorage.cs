using System.IO;
using CurrencyApp.Models;

namespace CurrencyApp.Services;

/// <summary>
/// Stores all saved days in a single JSON file.
/// </summary>
public class RatesStorage
{
    private readonly string _filePath;

    public RatesStorage(string filePath)//v konstruktoru vytvořím objekt s cestou k souboru, kam se budou ukládat DATA
    {
        _filePath = filePath;
    }

    public List<DailyRates> LoadAll()//vrací list objektů třídy DailyRates
    {
        if (!File.Exists(_filePath))//pokud neexistuje
            return [];//[] jsou zkratka pro new List<DailyRates>(), čili kniž to neexistije vrátí to prázdný list

        var json = File.ReadAllText(_filePath);//pokud existuje

        if (string.IsNullOrWhiteSpace(json))//pokud je prázdný
            return [];

        return JsonSerializer.Deserialize<List<DailyRates>>(json) ?? [];
        //JsonSerializer převádí z objektůna json a naopak
        //Deserialize převádí z JSON na OBJEKT
        //<TYP> na který má být json převeden
        //(vstup)
        //pokud tohle nejde(FALSE) ?? (TRUE)udělej tohle
        //[prázdný list]
    }

    public void Save(DailyRates dailyRates)//objekt třídy DailyRates(který chci uložit)
    {
        var all = LoadAll();//list "all", který obsahuje všechny dny ze souboru

        all.RemoveAll(x => x.Date.Date == dailyRates.Date.Date);
        //RemoveAll odstran vče co(xy)
        //LAMBDA který říká x je každý prvek v seznamu all(tj. jaždý objekt)
        //=> (v)
        //datumová hodnota proměnné date( Date.Date)
        //== se rovná
        // datumové promenné objektu co chci zrovna zapsat
        //BASUICALLY odstraní to vše co je stejné jako to co tam chci zapsat
        all.Add(dailyRates);

        var directory = Path.GetDirectoryName(_filePath);//najde cestu od kud je soubor
        if (!string.IsNullOrEmpty(directory)//pokud tam nějaká cesta je, directory není prázdný string
            Directory.CreateDirectory(directory);//pokud ta složka ještě neexistuje tak ji vytvoří(tak funguje CreateDirectory)
        //  TEPRVE SE TEN SOUBOR VYTVOŘÍ DLE "filePath"

        var json = JsonSerializer.Serialize(all, new JsonSerializerOptions {WriteIndented = true});
        //JsonSerializer převádí z objektu na json a naopak
        //serialize převádí z OBJEKTU na JSON
        //soubor co se bude serializovat(Objekt => soubor.Json)
        // new JsonSerializerOptions jsou možnosti serializace
        // WriteIndented je hezky odsazené a čitelné

        File.WriteAllText(_filePath, json);//zapíše text z jsonu do souboru, vytvoří ho(soubor) pokud neexistuje
    }
