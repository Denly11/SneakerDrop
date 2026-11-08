using ParkingApi.Dtos;
using ParkingApi.Entities;

namespace ParkingApi.Repositories;

public class ParkingPassRepository
{
    private readonly List<ParkingPass> _parkingPasses = new();

    public ParkingPass CreateParkingPass(CreateParkingPassRequest req, DateOnly validTo, int priceCzk)
    {
        var parkingPass = new ParkingPass
        {
            Id = _parkingPasses.Count + 1,
            ParkingLotId = req.ParkingLotId!.Value,
            LicensePlate = req.LicensePlate,
            HolderEmail = req.HolderEmail,
            ValidFrom = req.ValidFrom!.Value,
            DurationMonths = (int)req.DurationMonths!.Value,
            RequiresCharging = req.RequiresCharging!.Value,
            ValidTo = validTo,//<- metoda
            PriceCzk = priceCzk,//<- metoda
            CreatedAt = DateTime.UtcNow
        };
        _parkingPasses.Add(parkingPass);
        return parkingPass;
    }
    public List<ParkingPass> GetAll()
        => _parkingPasses.ToList();

    public List<ParkingPass> GetAllByParkingLotId(int parkingLotId)
        => _parkingPasses.Where(x => x.ParkingLotId == parkingLotId).ToList();
}
