namespace ParkingApi.Entities;

public class ParkingPass
{
    public int Id { get; set; }

    public int ParkingLotId { get; set; }

    public string LicensePlate { get; set; } = default!;

    public string HolderEmail { get; set; } = default!;

    public DateOnly ValidFrom  { get; set; }

    public int DurationMonths { get; set; }

    public bool RequiresCharging { get; set; }

    public DateOnly ValidTo { get; set; }

    public int PriceCzk { get; set; }

    public DateTime CreatedAt { get; set; }
}
