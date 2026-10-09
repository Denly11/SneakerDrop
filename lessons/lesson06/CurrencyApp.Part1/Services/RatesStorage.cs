using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using CurrencyApp.Part1.Models;

namespace CurrencyApp.Part1.Services;

public class RatesStorage
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        // Keeps Czech characters readable in the file ("Velká Británie" instead of "Velká ...").
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    private readonly string _filePath;

    public RatesStorage(string filePath)
    {
        _filePath = filePath;
    }

    public List<DailyRates> LoadAll()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        try
        {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<DailyRates>>(json, JsonOptions) ?? [];
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            throw new RatesStorageException("Uložené kurzy se nepodařilo načíst.", ex);
        }
    }

    public void Save(DailyRates dailyRates)
    {
        List<DailyRates> allDays = LoadAll();

        allDays.RemoveAll(day => day.Date.Date == dailyRates.Date.Date);
        allDays.Add(dailyRates);

        List<DailyRates> sortedDays = allDays.OrderByDescending(day => day.Date).ToList();

        try
        {
            File.WriteAllText(_filePath, JsonSerializer.Serialize(sortedDays, JsonOptions));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            throw new RatesStorageException("Kurzy se nepodařilo uložit.", ex);
        }
    }
}
