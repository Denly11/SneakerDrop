namespace CurrencyApp.Models;

/// <summary>
/// Exchange rate list published by the CNB for a single day.
/// </summary>
public class DailyRates
{
    public DateTime Date { get; set; }

    public List<CurrencyRate> Rates { get; set; } = [];
}
