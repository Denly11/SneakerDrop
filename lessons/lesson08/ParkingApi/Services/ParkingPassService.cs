using Microsoft.AspNetCore.DataProtection;
using ParkingApi.Dtos;
using ParkingApi.Entities;
using ParkingApi.Exceptions;
using ParkingApi.Repositories;

namespace ParkingApi.Services;

public class ParkingPassService
{
    private readonly ParkingPassRepository _parkingPassRepository;
    private readonly ParkingLotRepository _parkingLotRepository;

    public ParkingPassService(ParkingPassRepository parkingPassRepository, ParkingLotRepository parkingLotRepository)
    {
        _parkingPassRepository = parkingPassRepository;
        _parkingLotRepository = parkingLotRepository;
    }

    public ParkingPassResponse? CreateParkingPass(CreateParkingPassRequest req)
    {
        var parkingPassesForParkingLot = _parkingPassRepository.GetAllByParkingLotId(req.ParkingLotId!.Value);
        var validTo = CalculateValidTo(req.ValidFrom!.Value, (int)req.DurationMonths!.Value);
        req.LicensePlate = req.LicensePlate.Replace(" ", "").ToUpperInvariant();
        bool licensePlateAlreadyUsed = parkingPassesForParkingLot.Any(x => x.LicensePlate == req.LicensePlate && req.ValidFrom!.Value <= x.ValidTo && validTo >= x.ValidFrom);
        var parkingLot = _parkingLotRepository.GetParkingLot(req.ParkingLotId!.Value);


        if (parkingLot is null)//PASS-01
        {
            return null;
        }
        
        if (licensePlateAlreadyUsed == true)//PASS-02
        {
            throw new HandledException("License plate is already used on this parking lot.");
        }
        
        if (req.RequiresCharging == true && parkingLot.HasEvCharging == false)//PASS-03
        {
            throw new HandledException("Parking lot doest not have EV charging.");
        }

        var parkingPass = _parkingPassRepository.CreateParkingPass(req, validTo, CalculatePriceCzk((int)req.DurationMonths!.Value, parkingLot.HourlyRateCzk));
        return ToResponse(parkingPass);
    }
    public DateOnly CalculateValidTo(DateOnly validFrom, int durationMonths)
    {
        return validFrom.AddMonths(durationMonths).AddDays(-1);
    }

    public int CalculatePriceCzk(int durationMonths, decimal hourlyRateCzk)
    {
        decimal priceCzk = hourlyRateCzk * 100 * durationMonths;
        if (durationMonths == 12)
        {
            return (int)Math.Round(priceCzk*0.9m, MidpointRounding.AwayFromZero );//AwayFromZero => 0.5 se zaokrouhlí nahorun zatímco bez toho by se zaokrouhlina na nejbližžší sudé číslo, jenom poro 0.5
        }
        return (int)Math.Round(priceCzk, MidpointRounding.AwayFromZero);
    }

    private static ParkingPassResponse ToResponse(ParkingPass parkingPass)
    {
        return new ParkingPassResponse
        {
            Id = parkingPass.Id,
            ParkingLotId = parkingPass.ParkingLotId,
            LicensePlate = parkingPass.LicensePlate,
            HolderEmail = parkingPass.HolderEmail,
            ValidFrom = parkingPass.ValidFrom,
            DurationMonths = parkingPass.DurationMonths,
            RequiresCharging = parkingPass.RequiresCharging,
            ValidTo = parkingPass.ValidTo,
            PriceCzk = parkingPass.PriceCzk,
            CreatedAt = parkingPass.CreatedAt
        };
    }

    public List<ParkingPassResponse> Search(int? parkingLotId, string? licensePlate, DateOnly? validOn)
    {
        var parkingPasses = _parkingPassRepository.Search(parkingLotId, licensePlate, validOn);
        return parkingPasses.Select(ToResponse).ToList();
    }
}
