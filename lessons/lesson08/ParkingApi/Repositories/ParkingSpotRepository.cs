using ParkingApi.Dtos;
using ParkingApi.Entities;

namespace ParkingApi.Repositories;

public class ParkingSpotRepository
{
    private readonly List<ParkingSpot> _parkingSpots = new();

    public List<ParkingSpot> GetAllByParkingLotId(int parkingLotId)
        => _parkingSpots.Where(x => x.ParkingLotId == parkingLotId)
            .ToList();

    public ParkingSpot CreateParkingSpot(int parkingLotId, CreateParkingSpotRequest req)
    {
        var parkingSpot = new ParkingSpot
        {
            Id = _parkingSpots.Count + 1,
            ParkingLotId = parkingLotId,
            Label = req.Label,
            Type = req.Type!.Value
        };

        _parkingSpots.Add(parkingSpot);

        return parkingSpot;
    }
}
