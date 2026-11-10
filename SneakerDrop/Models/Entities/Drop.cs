namespace SneakerDrop.Models.Entities;

public class Drop
{
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime StartDate { get; set; }
}