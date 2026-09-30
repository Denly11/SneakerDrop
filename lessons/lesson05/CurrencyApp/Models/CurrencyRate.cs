namespace CurrencyApp.Models;

/// <summary>
/// One row of the CNB exchange rate table.
/// </summary>
public class CurrencyRate
{
    public string Country { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Amount { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal Rate { get; set; }
}
