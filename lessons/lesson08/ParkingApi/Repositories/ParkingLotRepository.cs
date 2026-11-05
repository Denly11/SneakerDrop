using System.Globalization;
using ParkingApi.Constants;
using ParkingApi.Dtos;
using ParkingApi.Entities;
using ParkingApi.Exceptions;

namespace ParkingApi.Repositories;

public class ParkingLotRepository
{
    private readonly List<ParkingLot> _parkingLots = new();

    public ParkingLot CreateParkingLot(ParkingLotRequest req)
    {
        var now = DateTime.UtcNow;

        var parkingLot = new ParkingLot
        {
            Id = _parkingLots.Count + 1,
            Name = req.Name,
            Capacity = req.Capacity!.Value,
            HourlyRateCzk = req.HourlyRateCzk!.Value,
            HasEvCharging = req.HasEvCharging!.Value,
            OpeningHours = CreateOpeningHours(req.OpeningHours),
            CreatedAt = now,
            UpdatedAt = now
        };

        _parkingLots.Add(parkingLot);

        return parkingLot;
    }

    public ParkingLot? GetParkingLot(string name)
        => _parkingLots.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

    public ParkingLot? GetParkingLot(int id)
        => _parkingLots.FirstOrDefault(x => x.Id == id);

    public List<ParkingLot> GetParkingLotsByName(string name)
        => _parkingLots.Where(x => x.Name.ToLower() == name.ToLower()).ToList();

    public ParkingLot UpdateParkingLot(int id, ParkingLotRequest req)
    {
        var parkingLot = GetParkingLot(id);

        if (parkingLot is null)
        {
            throw new HandledException($"Parking lot {id} was not found.");
        }

        parkingLot.Name = req.Name;
        parkingLot.Capacity = req.Capacity!.Value;
        parkingLot.HourlyRateCzk = req.HourlyRateCzk!.Value;
        parkingLot.HasEvCharging = req.HasEvCharging!.Value;
        parkingLot.OpeningHours = CreateOpeningHours(req.OpeningHours);
        parkingLot.UpdatedAt = DateTime.UtcNow;

        return parkingLot;
    }

    private static OpeningHours? CreateOpeningHours(OpeningHoursRequest? req)
    {
        if (req is null)
        {
            return null;
        }

        return new OpeningHours
        {
            Open = TimeOnly.ParseExact(req.Open, ParkingLotConstraints.OpeningHoursTimeFormat, CultureInfo.InvariantCulture),
            Close = TimeOnly.ParseExact(req.Close, ParkingLotConstraints.OpeningHoursTimeFormat, CultureInfo.InvariantCulture)
        };
    }
}
