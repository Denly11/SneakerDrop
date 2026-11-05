using ParkingApi.Entities;

namespace ParkingApi.Dtos;

public class ParkingSpotResponse
{
    public int Id { get; set; }

    public int ParkingLotId { get; set; }

    public string Label { get; set; } = default!;

    public ParkingSpotType Type { get; set; }
}
