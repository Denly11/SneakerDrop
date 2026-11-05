namespace ParkingApi.Dtos;

public class ParkingLotResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public int Capacity { get; set; }

    public decimal HourlyRateCzk { get; set; }

    public bool HasEvCharging { get; set; }

    public OpeningHoursResponse? OpeningHours { get; set; }

    public int FreeSpots { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
