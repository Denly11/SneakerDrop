using CurrencyApp.Models;

namespace CurrencyApp.Services;

/// <summary>
/// Stores all saved days in a single JSON file.
/// </summary>
public class RatesStorage
{
    private readonly string _filePath;

    public RatesStorage(string filePath)
    {
        _filePath = filePath;
    }

    public List<DailyRates> LoadAll()
    {
        // TODO
        throw new NotImplementedException();
    }

    public void Save(DailyRates dailyRates)
    {
        // TODO
        throw new NotImplementedException();
    }
}
