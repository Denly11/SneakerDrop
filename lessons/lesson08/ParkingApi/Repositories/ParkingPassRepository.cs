using System.Security.Cryptography.X509Certificates;
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
            ParkingLotId = req.ParkingLotId!.Value,//! oritiže request to chytí dřív než se to dostane sem
            LicensePlate = req.LicensePlate.Replace(" ", "").ToUpperInvariant(),
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

    public List<ParkingPass> GetAllByParkingLotId(int parkingLotId)
    {
        return _parkingPasses.Where(x => x.ParkingLotId == parkingLotId).ToList();
    }

    /*Filtr licensePlate se
    normalizuje stejně jako při vydání, validOn vrací permanentky platné
    v daný den. Výsledek je seřazený podle validFrom.*/

    public List<ParkingPass> Search(int? parkingLotId, string? licensePlate, DateOnly? validOn)
    {
        IEnumerable<ParkingPass> query = _parkingPasses;

        var normalizedLicensePlate = licensePlate?.Replace(" ", "").ToUpperInvariant();

        if (parkingLotId != null)
        {
            query = query.Where(x => x.ParkingLotId == parkingLotId);
        }

        if (normalizedLicensePlate != null)
        {
            query = query.Where(x => x.LicensePlate == normalizedLicensePlate);
        }

        if(validOn != null)
        {
            query = query.Where(x => x.ValidFrom <= validOn && x.ValidTo >= validOn);
        }

        query = query.OrderBy(x => x.ValidFrom);

        return query.ToList();
    }

    /*
     
    TOHLE JE UŽ BUSINESS LOGIKA
    public Boolean LicensePlateAlreadyUsedOnParkignLot(string licensePlate, int parkingLotId)
    {
        return _parkingPasses.Any(x => x.LicensePlate == licensePlate && x.ParkingLotId == parkingLotId);
    }
    */
}
