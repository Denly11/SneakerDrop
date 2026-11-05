using ParkingApi.Constants;
using ParkingApi.Dtos;
using ParkingApi.Entities;
using ParkingApi.Exceptions;
using ParkingApi.Repositories;

namespace ParkingApi.Services;

public class ParkingLotService
{
    private readonly ParkingLotRepository _parkingLotRepository;
    private readonly ParkingSpotRepository _parkingSpotRepository;

    public ParkingLotService(ParkingLotRepository parkingLotRepository, ParkingSpotRepository parkingSpotRepository)
    {
        _parkingLotRepository = parkingLotRepository;
        _parkingSpotRepository = parkingSpotRepository;
    }

    public ParkingLotResponse CreateParkingLot(ParkingLotRequest req)
    {
        if (_parkingLotRepository.GetParkingLot(req.Name) is not null)
        {
            throw new HandledException("Parking lot with this name already exists.");
        }

        var parkingLot = _parkingLotRepository.CreateParkingLot(req);

        return ToResponse(parkingLot, 0);
    }

    public ParkingLotResponse? GetParkingLotById(int id)
    {
        var parkingLot = _parkingLotRepository.GetParkingLot(id);

        if (parkingLot is null)
        {
            return null;
        }

        var occupiedSpots = _parkingSpotRepository.GetAllByParkingLotId(id).Count;

        return ToResponse(parkingLot, occupiedSpots);
    }

    public ParkingLotResponse? UpdateParkingLot(int id, ParkingLotRequest req)
    {
        var parkingLot = _parkingLotRepository.GetParkingLot(id);

        if (parkingLot is null)
        {
            return null;
        }

        if (_parkingLotRepository.GetParkingLotsByName(req.Name).Any(x => x.Id != id))
        {
            throw new HandledException("Parking lot with this name already exists.");
        }

        var occupiedSpots = _parkingSpotRepository.GetAllByParkingLotId(id).Count;

        if (occupiedSpots > req.Capacity)
        {
            throw new HandledException($"Capacity cannot be lower than the number of existing spots ({occupiedSpots}).");
        }

        var result = _parkingLotRepository.UpdateParkingLot(id, req);

        return ToResponse(result, occupiedSpots);
    }

    public ParkingSpotResponse? CreateParkingSpot(int parkingLotId, CreateParkingSpotRequest req)
    {
        var parkingLot = _parkingLotRepository.GetParkingLot(parkingLotId);

        if (parkingLot is null)
        {
            return null;
        }

        if (req.Type == ParkingSpotType.Electric && !parkingLot.HasEvCharging)
        {
            throw new HandledException("Electric spot can only be added to a parking lot with EV charging.");
        }

        var occupiedSpots = _parkingSpotRepository.GetAllByParkingLotId(parkingLotId).Count;

        if (occupiedSpots >= parkingLot.Capacity)
        {
            throw new HandledException("Parking lot capacity is full.");
        }

        req.Label = req.Label.ToUpper();

        var parkingSpot = _parkingSpotRepository.CreateParkingSpot(parkingLotId, req);

        return new ParkingSpotResponse
        {
            Id = parkingSpot.Id,
            ParkingLotId = parkingSpot.ParkingLotId,
            Label = parkingSpot.Label,
            Type = parkingSpot.Type
        };
    }

    private static ParkingLotResponse ToResponse(ParkingLot parkingLot, int occupiedSpots)
        => new()
        {
            Id = parkingLot.Id,
            Name = parkingLot.Name,
            Capacity = parkingLot.Capacity,
            HourlyRateCzk = parkingLot.HourlyRateCzk,
            HasEvCharging = parkingLot.HasEvCharging,
            OpeningHours = parkingLot.OpeningHours is null ? null : new OpeningHoursResponse
            {
                Open = parkingLot.OpeningHours.Open.ToString(ParkingLotConstraints.OpeningHoursTimeFormat),
                Close = parkingLot.OpeningHours.Close.ToString(ParkingLotConstraints.OpeningHoursTimeFormat)
            },
            FreeSpots = parkingLot.Capacity - occupiedSpots,
            CreatedAt = parkingLot.CreatedAt,
            UpdatedAt = parkingLot.UpdatedAt
        };
}
