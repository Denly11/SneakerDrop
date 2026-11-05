namespace ParkingApi.Entities;

public class ParkingLot
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public int Capacity { get; set; }

    public decimal HourlyRateCzk { get; set; }

    public bool HasEvCharging { get; set; }

    // null = nonstop provoz
    public OpeningHours? OpeningHours { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
